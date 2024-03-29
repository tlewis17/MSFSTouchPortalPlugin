﻿using System;

namespace MSFSTouchPortalPlugin.Helpers
{
  /// <summary>
  /// Internal timer type for interaction with SimConnect.
  /// This is to ensure that async calls always run on the same thread,
  /// because SimConnect often crashes due to threading issues with regular timers.
  /// Note that this timer type needs an event loop to call the Tick() method periodically.
  /// </summary>
  public class UnthreadedTimer : IDisposable
  {
    public event EventHandler Elapsed = null;

    public bool AutoReset { get; set; } = true;
    public int Interval
    {
      get { return interval; }
      set { interval = Math.Max(value, 1); UpdateNextTick(); }
    }
    public bool Enabled
    {
      get { return enabled; }
      set { enabled = value; UpdateNextTick(); }
    }

    private bool enabled = false;
    private int interval = 1000;
    private int nextTick = 0;

    public UnthreadedTimer() { }
    public UnthreadedTimer(int interval) {
      Interval = interval;
    }

    public void Tick() {
      if (enabled && Environment.TickCount >= nextTick) {
        enabled = false;
        if (Elapsed != null)
          Elapsed.Invoke(this, EventArgs.Empty);
        if (AutoReset)
          Start();
      }
    }

    public void Start() {
      Enabled = true;
    }

    public void Stop() {
      Enabled = false;
    }

    private void UpdateNextTick() {
      nextTick = Environment.TickCount + interval;
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      if (disposing) {
        Stop();
        Elapsed = null;
      }
    }

  }
}
