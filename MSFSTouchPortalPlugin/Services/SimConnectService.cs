using Microsoft.Extensions.Logging;
using Microsoft.FlightSimulator.SimConnect;
using MSFSTouchPortalPlugin.Constants;
using MSFSTouchPortalPlugin.Enums;
using MSFSTouchPortalPlugin.Interfaces;
using MSFSTouchPortalPlugin.Objects.AutoPilot;
using MSFSTouchPortalPlugin.Objects.InstrumentsSystems;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MSFSTouchPortalPlugin.Services {
  /// <summary>
  /// Wrapper for SimConnect
  /// </summary>
  internal class SimConnectService : ISimConnectService, IDisposable {
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    private readonly ILogger<SimConnectService> _logger;

    const uint NOTIFICATION_PRIORITY = 10000000;
    const int WM_USER_SIMCONNECT = 0x0402;
    SimConnect _simConnect = null;
    readonly EventWaitHandle _scReady = new EventWaitHandle(false, EventResetMode.AutoReset);
    private bool _connected = false;
    
    public event DataUpdateEventHandler OnDataUpdateEvent;
    public event ConnectEventHandler OnConnect;
    public event DisconnectEventHandler OnDisconnect;

    public SimConnectService(ILogger<SimConnectService> logger) {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public bool IsConnected() => _connected;

    public bool Connect() {
      _logger.LogInformation("Connect SimConnect");

      try {
        _simConnect = new SimConnect("Touch Portal Plugin", GetConsoleWindow(), WM_USER_SIMCONNECT, _scReady, 0);

        _connected = true;

        // System Events
        _simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen);
        _simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit);
        _simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException);

        // Sim mapped events
        _simConnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(simconnect_OnRecvEvent);

        // Sim Data
        _simConnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simconnect_OnRecvSimObjectData);
        _simConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(simconnect_OnRecvSimobjectDataBytype);

        _simConnect.ClearNotificationGroup(Groups.System);
        _simConnect.SetNotificationGroupPriority(Groups.System, NOTIFICATION_PRIORITY);

        _simConnect.ClearNotificationGroup(Groups.AutoPilot);
        _simConnect.SetNotificationGroupPriority(Groups.AutoPilot, NOTIFICATION_PRIORITY);

        _simConnect.ClearNotificationGroup(Groups.Fuel);
        _simConnect.SetNotificationGroupPriority(Groups.Fuel, NOTIFICATION_PRIORITY);

        _simConnect.Text(SIMCONNECT_TEXT_TYPE.PRINT_BLACK, 5, Events.StartupMessage, "TouchPortal Connected");

        // Invoke Handler
        OnConnect();

        return true;
      } catch (COMException ex) {
        _logger.LogError("Connection to Sim failed: {exception}", ex.Message);
      }

      return false;
    }

    public void Disconnect() {
      _logger.LogInformation("Disconnect SimConnect");

      if (_simConnect != null) {
        /// Dispose serves the same purpose as SimConnect_Close()
        _simConnect.Dispose();
        _simConnect = null;
      }

      _connected = false;
      
      // Invoke Handler
      OnDisconnect();
    }

    public Task WaitForMessage(CancellationToken cancellationToken) {
      while (_connected && !cancellationToken.IsCancellationRequested) {
        if (_scReady.WaitOne(TimeSpan.FromSeconds(5))) {
          // TODO: Exception on quit
          _simConnect?.ReceiveMessage();
        }
      }

      return Task.CompletedTask;
    }

    public bool MapClientEventToSimEvent(Enum eventId, string eventName) {
      if (_connected) {
        _simConnect.MapClientEventToSimEvent(eventId, eventName);
        return true;
      }

      return false;
    }

    public bool TransmitClientEvent(Groups group, Enum eventId, uint data) {
      if (_connected) {
        _simConnect.TransmitClientEvent((uint)SimConnect.SIMCONNECT_OBJECT_ID_USER, eventId, data, group, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
        return true;
      }

      return false;
    }

    public bool AddNotification(Enum group, Enum eventId) {
      if (_connected) {
        _simConnect.AddClientEventToNotificationGroup(group, eventId, false);
        return true;
      }

      return false;
    }

    public bool RegisterToSimConnect(SimVarItem simVar) {
      if (_connected) {
        _simConnect.AddToDataDefinition(simVar.def, simVar.SimVarName, simVar.Unit, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
        _simConnect.RegisterDataDefineStruct<double>(simVar.def);
        return true;
      }

      return false;
    }

    public bool RequestDataOnSimObjectType(SimVarItem simVar) {
      if (_connected) {
        _simConnect.RequestDataOnSimObjectType(simVar.def, simVar.def, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
      }

      return false;
    }

    private void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data) {
      _logger.LogInformation("Quit");
      Disconnect();
    }

    private void simconnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data) {
      if (data.dwData.Length > 0) {
        OnDataUpdateEvent((Definition)data.dwDefineID, (Definition)data.dwRequestID, data.dwData[0]);
      }

    }

    private void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data) {
      _logger.LogInformation("Opened");
    }

    private void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data) {
      SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
      _logger.LogError("SimConnect_OnRecvException: {exception}", eException.ToString());
    }

    /// <summary>
    /// Events triggered by sending events to the Sim
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    private void simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT data) {
      Groups group = (Groups)data.uGroupID;
      dynamic eventId = null;

      switch (group) {
        case Groups.System:
          eventId = (Events)data.uEventID;
          break;
        case Groups.AutoPilot:
          eventId = (AutoPilot)data.uEventID;
          break;
        case Groups.Fuel:
          eventId = (Fuel)data.uEventID;
          break;
        default:
          // No other actions
          break;
      }

      _logger.LogInformation($"{DateTime.Now} Recieved: {group} - {eventId}");
    }

    private void simconnect_OnRecvSimObjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data) {
      // Empty method for now, not implemented
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects).
          _scReady.Dispose();
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~SimConnectService()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose() {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}
