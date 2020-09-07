﻿using MSFSTouchPortalPlugin.Attributes;
using TouchPortalExtension.Attributes;

namespace MSFSTouchPortalPlugin.Objects.Menu {
  [TouchPortalCategory("Menu", "MFS - Menu")]
  internal class MenuMapping {
    //[TouchPortalAction("Pause", "Pause", "MSFS", "Toggle/On/Off Pause", "Pause - {0}")]
    //[TouchPortalActionChoice(new string[] { "Toggle", "On", "Off" }, "Toggle")]
    //public object PAUSE { get; }
  }

  [SimNotificationGroup(SimConnectWrapper.Groups.Menu)]
  [TouchPortalCategoryMapping("Menu")]
  internal enum Menu {
    // Placeholder to offset each enum for SimConnect
    Init = 3000,
  }
}
