﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSFSTouchPortalPlugin.Attributes;
using MSFSTouchPortalPlugin.Constants;
using MSFSTouchPortalPlugin.Interfaces;
using MSFSTouchPortalPlugin.Objects.Plugin;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TouchPortalSDK;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Events;
using TouchPortalSDK.Messages.Models;
using Timer = MSFSTouchPortalPlugin.Helpers.UnthreadedTimer;

namespace MSFSTouchPortalPlugin.Services {
  /// <inheritdoc cref="IPluginService" />
  internal class PluginService : IPluginService, IDisposable, ITouchPortalEventHandler {
    private CancellationToken _cancellationToken;
    private CancellationTokenSource _simConnectCancellationTokenSource;

    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<PluginService> _logger;
    private readonly ITouchPortalClient _client;
    private readonly ISimConnectService _simConnectService;
    private readonly IReflectionService _reflectionService;

    public string PluginId => "MSFSTouchPortalPlugin";
    private readonly IReadOnlyCollection<Setting> _settings;
    /// <summary>
    /// milliseconds for repeat (held) actions; Perhaps this could be a setting for this plugin.
    /// </summary>
    public int ActionRepeatInterval = 450;

    private Dictionary<string, Enum> actionsDictionary = new Dictionary<string, Enum>();
    private Dictionary<Definition, SimVarItem> statesDictionary = new Dictionary<Definition, SimVarItem>();
    private Dictionary<string, Enum> internalEventsDictionary = new Dictionary<string, Enum>();

    private readonly ConcurrentDictionary<string, Timer> repeatingActionTimers = new();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageProcessor">Message Processor Object</param>
    public PluginService(IHostApplicationLifetime hostApplicationLifetime, ILogger<PluginService> logger,
      ITouchPortalClientFactory clientFactory, ISimConnectService simConnectService, IReflectionService reflectionService) {
      _hostApplicationLifetime = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _simConnectService = simConnectService ?? throw new ArgumentNullException(nameof(simConnectService));
      _reflectionService = reflectionService ?? throw new ArgumentNullException(nameof(reflectionService));

      _client = clientFactory?.Create(this) ?? throw new ArgumentNullException(nameof(clientFactory));
    }

    /// <summary>
    /// Runs the plugin services
    /// </summary>
    /// <param name="simConnectCancelToken">The Cancellation Token</param>
    public async Task RunPluginServices(CancellationToken simConnectCancelToken) {
      // Run Data Polling and repeating actions timers
      var runTimersTask = RunTimedEventsTask(simConnectCancelToken);

      await Task.WhenAll(new Task[] {
        runTimersTask,
        // Run Listen and pairing
        _simConnectService.WaitForMessage(simConnectCancelToken)
      }).ConfigureAwait(false);
    }

    /// <summary>
    /// Async task for running various timed SimConnect events on one thread.
    /// This is primarily used for data polling and repeating (held) actions.
    /// </summary>
    /// <param name="cancellationToken">The Cancellation Token</param>
    private async Task RunTimedEventsTask(CancellationToken cancellationToken) {
      // Run Data Polling
      var dataPollTimer = new Timer(250);
      dataPollTimer.Elapsed += delegate { CheckPendingRequests(); };
      dataPollTimer.Start();

      while (_simConnectService.IsConnected() && !cancellationToken.IsCancellationRequested) {
        dataPollTimer.Tick();
        foreach (Timer tim in repeatingActionTimers.Values)
          tim.Tick();
        await Task.Delay(20, cancellationToken);
      }

      dataPollTimer.Dispose();
    }

    /// <summary>
    /// Starts the plugin service
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    public Task StartAsync(CancellationToken cancellationToken) {
      _cancellationToken = cancellationToken;

      _hostApplicationLifetime.ApplicationStarted.Register(() => {
        if (!Initialize()) {
          _hostApplicationLifetime.StopApplication();
          return;
        }

        SetupEventLists();
        Task.WhenAll(TryConnect());
      });

      _hostApplicationLifetime.ApplicationStopping.Register(() => {
        // Disconnect from SimConnect
        _simConnectService.Disconnect();
      });

      return Task.CompletedTask;
    }

    /// <summary>
    /// Stops the plugin service
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    public Task StopAsync(CancellationToken cancellationToken) {
      return Task.CompletedTask;
    }

    #region IDisposable Support
    private bool disposedValue; // To detect redundant calls

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // Dispose managed state (managed objects).
          _simConnectCancellationTokenSource?.Dispose();
        }

        disposedValue = true;
      }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose() {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion

    /// <summary>
    /// Initialized the Touch Portal Message Processor
    /// </summary>
    private bool Initialize() {
      if (!_client.Connect()) {
        return false;
      }

      // Setup SimConnect Events
      _simConnectService.OnDataUpdateEvent += SimConnectEvent_OnDataUpdateEvent;
      _simConnectService.OnConnect += SimConnectEvent_OnConnect;
      _simConnectService.OnDisconnect += SimConnectEvent_OnDisconnect;

      return true;
    }

    #region SimConnect Events

    private void SimConnectEvent_OnConnect() {
      _simConnectCancellationTokenSource = new CancellationTokenSource();

      _client.StateUpdate("MSFSTouchPortalPlugin.Plugin.State.Connected", _simConnectService.IsConnected().ToString().ToLower());

      // Register Actions
      foreach (var a in actionsDictionary) {
        _simConnectService.MapClientEventToSimEvent(a.Value, a.Value.ToString());
        _simConnectService.AddNotification(a.Value.GetType().GetCustomAttribute<SimNotificationGroupAttribute>().Group, a.Value);
      }

      // Register SimVars
      foreach (var s in statesDictionary) {
        _simConnectService.RegisterToSimConnect(s.Value);
      }

      Task.WhenAll(RunPluginServices(_simConnectCancellationTokenSource.Token));
    }

    private void SimConnectEvent_OnDataUpdateEvent(Definition def, Definition req, object data) {
      // Lookup State Mapping
      if (statesDictionary.TryGetValue(def, out var value)) {
        var stringVal = data.ToString();

        // Only update state on changes
        // TODO: Move these to after parsing due to fractional unnoticable changes.
        if (value.Value != stringVal) {
          value.Value = stringVal;
          object valObj = stringVal;

          // Handle conversions
          if (Units.ShouldConvertToFloat(value.Unit)) {
            valObj = float.Parse(stringVal);
          } else if (value.Unit == Units.String) {
            valObj = ((StringVal64)data).Value;
          } else if (value.Unit == Units.radians) {
            // Convert to Degrees
            valObj = float.Parse(stringVal) * (180 / Math.PI);
          }

          // Update if known id.
          if (!string.IsNullOrWhiteSpace(value.TouchPortalStateId)) {
            _client.StateUpdate(value.TouchPortalStateId, string.Format(value.StringFormat, valObj));
          }
        }

        value.SetPending(false);
      }
    }

    private void SimConnectEvent_OnDisconnect() {
      _simConnectCancellationTokenSource.Cancel();
      _client.StateUpdate("MSFSTouchPortalPlugin.Plugin.State.Connected", _simConnectService.IsConnected().ToString().ToLower());
    }

    #endregion

    private void SetupEventLists() {
      internalEventsDictionary = _reflectionService.GetInternalEvents();
      actionsDictionary = _reflectionService.GetActionEvents();
      statesDictionary = _reflectionService.GetStates();
    }

    private Task TryConnect() {
      int i = 0;

      while (!_cancellationToken.IsCancellationRequested) {
        if (i == 0 && !_simConnectService.IsConnected()) {
          _simConnectService.Connect();
          i = 10;
        }

        // SimConnect is typically available even before loading into a flight. This should connect and be ready by the time a flight is started.
        i--;
        Thread.Sleep(1000);
      }

      return Task.CompletedTask;
    }

    private void ProcessEvent(string actionId, string value = default) {
      // Plugin Events
      if (internalEventsDictionary.TryGetValue($"{actionId}:{value}", out var internalEventResult)) {
        _logger.LogInformation($"{DateTime.Now} {internalEventResult} - Firing Internal Event");

        switch (internalEventResult) {
          case Plugin.ToggleConnection:
            if (_simConnectService.IsConnected()) {
              _simConnectService.Disconnect();
            } else {
              _simConnectService.Connect();
            }
            break;
          case Plugin.Connect:
            if (!_simConnectService.IsConnected()) {
              _simConnectService.Connect();
            }
            break;
          case Plugin.Disconnect:
            if (_simConnectService.IsConnected()) {
              _simConnectService.Disconnect();
            }
            break;
          default:
            // No other types of events supported right now.
            break;
        }

        return;
      }

      // Sim Events
      if (actionsDictionary.TryGetValue($"{actionId}:{value}", out var eventResult)) {
        _logger.LogInformation($"{DateTime.Now} {eventResult} - Firing Event");
        var group = eventResult.GetType().GetCustomAttribute<SimNotificationGroupAttribute>().Group;
        _simConnectService.TransmitClientEvent(group, eventResult, 0);
      }
    }

    private void CheckPendingRequests() {
      foreach (var s in statesDictionary) {
        // Expire pending if more than 30 seconds
        s.Value.PendingTimeout();

        // Check if Pending data request in paly
        if (!s.Value.PendingRequest) {
          _simConnectService.RequestDataOnSimObjectType(s.Value);
          s.Value.SetPending(true);
        }
      }
    }

    private void ClearRepeatingActions() {
      foreach (var act in repeatingActionTimers) {
        if (repeatingActionTimers.TryRemove(act.Key, out var tim)) {
          tim.Dispose();
        }
      }
    }

    #region TouchPortalSDK Events

    public void OnInfoEvent(InfoEvent message) {
      _logger.LogInformation(
        $"[Info] VersionCode: '{message.TpVersionCode}', VersionString: '{message.TpVersionString}', SDK: '{message.SdkVersion}', PluginVersion: '{message.PluginVersion}', Status: '{message.Status}'"
      );
    }

    public void OnListChangedEvent(ListChangeEvent message) {
      throw new NotImplementedException();
    }

    public void OnBroadcastEvent(BroadcastEvent message) {
      throw new NotImplementedException();
    }

    public void OnSettingsEvent(SettingsEvent message) {
      throw new NotImplementedException();
    }

    public void OnActionEvent(ActionEvent message)
    {
      //_logger.LogInformation($"ACTION {message.Type}");
      string values = default;
      if (message.Data.Count > 0)
        values = string.Join(",", message.Data.Select(x => x.Value));

      // Actions used in TP "On Hold" events will send "down" and "up" events, in that order (usually).
      // "On Pressed" actions will have an "action" event. These are all abstracted into TouchPortalSDK enums.
      // Note that an "On Hold" action ("down" event) is _not_ triggered upon initial press. This allow different
      // actions per button, and button designer can still choose to duplicate the Hold action in the Pressed handler.
      switch (message.GetPressState()) {
        case TouchPortalSDK.Messages.Models.Enums.Press.Down:
          // "On Hold" activated ("down" event). Try to add this action to the repeating/scheduled actions queue, unless it already exists.
          var timer = new Timer(ActionRepeatInterval);
          timer.Elapsed += delegate { ProcessEvent(message.ActionId, values); };
          if (repeatingActionTimers.TryAdd(message.ActionId, timer))
            timer.Start();
          else
            timer.Dispose();
          break;

        case TouchPortalSDK.Messages.Models.Enums.Press.Up:
          // "On Hold" released ("up" event). Mark action for removal from repeating queue.
          if (repeatingActionTimers.TryRemove(message.ActionId, out var tim))
            tim.Dispose();
          // No further processing for this action.
          break;

        case TouchPortalSDK.Messages.Models.Enums.Press.Tap:
          // Process an "On Pressed" ("action" event) action.
          ProcessEvent(message.ActionId, values);
          break;
      }
    }

    public void OnClosedEvent(string message) {
      _logger?.LogInformation("TouchPortal Disconnected.");

      //Optional force exits this plugin.
      Environment.Exit(0);
    }

    public void OnUnhandledEvent(string jsonMessage) {
      var jsonDocument = JsonSerializer.Deserialize<JsonDocument>(jsonMessage);
      _logger?.LogWarning($"Unhandled message: {jsonDocument}");
    }
    #endregion
  }
}
