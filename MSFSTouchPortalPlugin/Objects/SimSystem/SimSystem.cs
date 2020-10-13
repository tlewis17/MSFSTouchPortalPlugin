using MSFSTouchPortalPlugin.Attributes;
using MSFSTouchPortalPlugin.Constants;
using MSFSTouchPortalPlugin.Enums;
using TouchPortalExtension.Attributes;

namespace MSFSTouchPortalPlugin.Objects.SimSystem {
  [SimVarDataRequestGroup]
  [TouchPortalCategory("SimSystem", "MSFS - System")]
  internal static class SimSystemMapping {
    //[TouchPortalAction("Pause", "Pause", "MSFS", "Toggle/On/Off Pause", "Pause - {0}")]
    //[TouchPortalActionChoice(new [] { "Toggle", "On", "Off" }, "Toggle")]
    //public object PAUSE { get; }

    [SimVarDataRequest]
    [TouchPortalAction("SimulationRate", "Simulation Rate", "MSFS", "Simulation Rate", "Rate {0}")]
    [TouchPortalActionChoice(new [] { "Increase", "Decrease" }, "Decrease")]
    [TouchPortalState("SimulationRate", "text", "The current simulation rate", "")]
    public static readonly SimVarItem SimulationRate =
      new SimVarItem { def = Definition.SimulationRate, SimVarName = "SIMULATION RATE", Unit = Units.number, CanSet = false };
  }

  [SimNotificationGroup(Groups.SimSystem)]
  [TouchPortalCategoryMapping("SimSystem")]
  internal enum SimSystem {
    // Placeholder to offset each enum for SimConnect
    Init = 3000,

    #region SimRate

    [SimActionEvent]
    [TouchPortalActionMapping("SimulationRate", "Increase")]
    SIM_RATE_INCR,
    [SimActionEvent]
    [TouchPortalActionMapping("SimulationRate", "Decrease")]
    SIM_RATE_DECR

    #endregion
  }
}
