using MSFSTouchPortalPlugin.Attributes;
using MSFSTouchPortalPlugin.Constants;
using MSFSTouchPortalPlugin.Enums;
using TouchPortalExtension.Attributes;

namespace MSFSTouchPortalPlugin.Objects.InstrumentsSystems {
  [SimVarDataRequestGroup]
  [TouchPortalCategory("FlightInstruments", "MSFS - Flight Instruments")]
  internal static class FlightInstrumentsMapping {

    #region Velocity

    [SimVarDataRequest]
    [TouchPortalState("GroundVelocity", "text", "Ground Speed in Knots", "")]
    public static readonly SimVarItem GroundVelocity = new SimVarItem { Def = Definition.GroundVelocity, SimVarName = "GROUND VELOCITY", Unit = Units.knots, CanSet = false, StringFormat = "{0:0.0#}" };

    [SimVarDataRequest]
    [TouchPortalState("AirSpeedTrue", "text", "Air speed true in Knots", "")]
    public static readonly SimVarItem AirSpeedTrue = new SimVarItem { Def = Definition.AirSpeedTrue, SimVarName = "AIRSPEED TRUE", Unit = Units.knots, CanSet = true, StringFormat = "{0:0.0#}" };

    [SimVarDataRequest]
    [TouchPortalState("AirSpeedIndicated", "text", "Air speed indicated in Knots", "")]
    public static readonly SimVarItem AirSpeedIndicated = new SimVarItem { Def = Definition.AirSpeedIndicated, SimVarName = "AIRSPEED INDICATED", Unit = Units.knots, CanSet = true, StringFormat = "{0:0.0#}" };

    [SimVarDataRequest]
    [TouchPortalState("AirSpeedMach", "text", "Air speed indicated in mach", "")]
    public static readonly SimVarItem AirSpeedMach = new SimVarItem { Def = Definition.AirSpeedMach, SimVarName = "AIRSPEED MACH", Unit = Units.mach, CanSet = true, StringFormat = "{0:0.0#}" };
    #endregion

    #region Altitude

    [SimVarDataRequest]
    [TouchPortalState("PlaneAltitude", "text", "Plane Altitude in Feet", "")]
    public static readonly SimVarItem PlaneAltitude = new SimVarItem { Def = Definition.PlaneAltitude, SimVarName = "PLANE ALTITUDE", Unit = Units.feet, CanSet = false, StringFormat = "{0:0.#}" };

    [SimVarDataRequest]
    [TouchPortalState("PlaneAltitudeAGL", "text", "Plane Altitude AGL in Feet", "")]
    public static readonly SimVarItem PlaneAltitudeAGL = new SimVarItem { Def = Definition.PlaneAltitudeAGL, SimVarName = "PLANE ALT ABOVE GROUND", Unit = Units.feet, CanSet = false, StringFormat = "{0:0.#}" };

    [SimVarDataRequest]
    [TouchPortalState("GroundAltitude", "text", "Ground level in Feet", "")]
    public static readonly SimVarItem GroundAltitude = new SimVarItem { Def = Definition.GroundAltitude, SimVarName = "GROUND ALTITUDE", Unit = Units.feet, CanSet = false, StringFormat = "{0:0.#}" };

    #endregion

    #region Heading

    [SimVarDataRequest]
    [TouchPortalState("PlaneHeadingTrue", "text", "Plane Heading (True North) in Degrees", "")]
    public static readonly SimVarItem PlaneHeadingTrue = new SimVarItem { Def = Definition.PlaneHeadingTrue, SimVarName = "PLANE HEADING DEGREES TRUE", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    [SimVarDataRequest]
    [TouchPortalState("PlaneHeadingMagnetic", "text", "Plane Heading (Magnetic North) in Degrees", "")]
    public static readonly SimVarItem PlaneHeadingMagnetic = new SimVarItem { Def = Definition.PlaneHeadingMagnetic, SimVarName = "PLANE HEADING DEGREES MAGNETIC", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    #endregion

    #region Bank and Pitch

    [SimVarDataRequest]
    [TouchPortalState("PlaneBankAngle", "text", "Plane Bank Angle in Degrees", "")]
    public static readonly SimVarItem PlaneBankAngle = new SimVarItem { Def = Definition.PlaneBankAngle, SimVarName = "PLANE BANK DEGREES", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    [SimVarDataRequest]
    [TouchPortalState("PlanePitchAngle", "text", "Plane Pitch Angle in Degrees", "")]
    public static readonly SimVarItem PlanePitchAngle = new SimVarItem { Def = Definition.PlanePitchAngle, SimVarName = "PLANE PITCH DEGREES", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    [SimVarDataRequest]
    [TouchPortalState("VerticalSpeed", "text", "Vertical Speed in feet per minute", "")]
    public static readonly SimVarItem VerticalSpeed = new SimVarItem { Def = Definition.VerticalSpeed, SimVarName = "VERTICAL SPEED", Unit = Units.feetminute, CanSet = true, StringFormat = "{0:0.0#}" };

    #endregion

    #region Warnings

    [SimVarDataRequest]
    [TouchPortalState("StallWarning", "text", "Stall Warning true/false", "")]
    public static readonly SimVarItem StallWarning = new SimVarItem { Def = Definition.StallWarning, SimVarName = "STALL WARNING", Unit = Units.Bool, CanSet = false };

    [SimVarDataRequest]
    [TouchPortalState("OverspeedWarning", "text", "Overspeed Warning true/false", "")]
    public static readonly SimVarItem OverspeedWarning = new SimVarItem { Def = Definition.OverspeedWarning, SimVarName = "OVERSPEED WARNING", Unit = Units.Bool, CanSet = false };

    [SimVarDataRequest]
    [TouchPortalState("FlapSpeedExceeeded", "text", "Flap Speed Exceeded Warning true/false", "")]
    public static readonly SimVarItem FlapSpeedExceeeded = new SimVarItem { Def = Definition.FlapSpeedExceeeded, SimVarName = "FLAP SPEED EXCEEDED", Unit = Units.Bool, CanSet = false };

    #endregion

    #region G1000

    [TouchPortalAction("G1000", "G1000 Display", "MSFS", "Pushes the LCD display button", "{0} - {1}")]
    [TouchPortalActionChoice(new[] { "PFD", "MFD" }, "PFD")]
    [TouchPortalActionChoice(new[] { "Flightplan", "Procedure", "Zoom In", "Zoom Out", "Direct To", "Menu", "Clear", "Enter", "Cursor", "Group Knob Increase", "Group Knob Decrease",
    "Page Knob Increase", "Page Knob Decrease", "Key1", "Key2", "Key3", "Key4", "Key5", "Key6", "Key7", "Key8", "Key9", "Key10", "Key11", "Key12" }, "Key1")]
    public static object G1000 { get; }

    #endregion

  }

  [SimNotificationGroup(Groups.FlightInstruments)]
  [TouchPortalCategoryMapping("FlightInstruments")]
  internal enum FlightInstruments {
    // Placeholder to offset each enum for SimConnect
    Init = 7000,

    #region G1000

    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Flightplan" })]
    G1000_PFD_FLIGHTPLAN_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Procedure" })]
    G1000_PFD_PROCEDURE_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Zoom In" })]
    G1000_PFD_ZOOMIN_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Zoom Out" })]
    G1000_PFD_ZOOMOUT_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Direct To" })]
    G1000_PFD_DIRECTTO_BUTTON,
    G1000_PFD_MENU_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Clear" })]
    G1000_PFD_CLEAR_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Enter" })]
    G1000_PFD_ENTER_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Cursor" })]
    G1000_PFD_CURSOR_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Group Knob Increase" })]
    G1000_PFD_GROUP_KNOB_INC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Group Knob Decrease" })]
    G1000_PFD_GROUP_KNOB_DEC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Page Knob Increase" })]
    G1000_PFD_PAGE_KNOB_INC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Page Knob Decrease" })]
    G1000_PFD_PAGE_KNOB_DEC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key1" })]
    G1000_PFD_SOFTKEY1,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key2" })]
    G1000_PFD_SOFTKEY2,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key3" })]
    G1000_PFD_SOFTKEY3,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key4" })]
    G1000_PFD_SOFTKEY4,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key5" })]
    G1000_PFD_SOFTKEY5,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key6" })]
    G1000_PFD_SOFTKEY6,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key7" })]
    G1000_PFD_SOFTKEY7,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key8" })]
    G1000_PFD_SOFTKEY8,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key9" })]
    G1000_PFD_SOFTKEY9,
    G1000_PFD_SOFTKEY10,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key11" })]
    G1000_PFD_SOFTKEY11,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key12" })]
    G1000_PFD_SOFTKEY12,

    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Flightplan" })]
    G1000_MFD_FLIGHTPLAN_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Procedure" })]
    G1000_MFD_PROCEDURE_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Zoom In" })]
    G1000_MFD_ZOOMIN_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Zoom Out" })]
    G1000_MFD_ZOOMOUT_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Direct To" })]
    G1000_MFD_DIRECTTO_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Menu" })]
    G1000_MFD_MENU_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Clear" })]
    G1000_MFD_CLEAR_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Enter" })]
    G1000_MFD_ENTER_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Cursor" })]
    G1000_MFD_CURSOR_BUTTON,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Group Knob Increase" })]
    G1000_MFD_GROUP_KNOB_INC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Group Knob Decrease" })]
    G1000_MFD_GROUP_KNOB_DEC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Page Knob Increase" })]
    G1000_MFD_PAGE_KNOB_INC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Page Knob Decrease" })]
    G1000_MFD_PAGE_KNOB_DEC,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key1" })]
    G1000_MFD_SOFTKEY1,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key2" })]
    G1000_MFD_SOFTKEY2,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key3" })]
    G1000_MFD_SOFTKEY3,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key4" })]
    G1000_MFD_SOFTKEY4,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key5" })]
    G1000_MFD_SOFTKEY5,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key6" })]
    G1000_MFD_SOFTKEY6,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key7" })]
    G1000_MFD_SOFTKEY7,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key8" })]
    G1000_MFD_SOFTKEY8,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key9" })]
    G1000_MFD_SOFTKEY9,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key10" })]
    G1000_MFD_SOFTKEY10,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key11" })]
    G1000_MFD_SOFTKEY11,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "MFD", "Key12" })]
    G1000_MFD_SOFTKEY12,

    #endregion

    #region Mobiflight G1000

    #region PFD

    MOBIFLIGHT_AS1000_PFD_VOL_1_INC,
    MOBIFLIGHT_AS1000_PFD_VOL_1_DEC,
    MOBIFLIGHT_AS1000_PFD_VOL_2_INC,
    MOBIFLIGHT_AS1000_PFD_VOL_2_DEC,
    MOBIFLIGHT_AS1000_PFD_NAV_Switch,
    MOBIFLIGHT_AS1000_PFD_NAV_Large_INC,
    MOBIFLIGHT_AS1000_PFD_NAV_Large_DEC,
    MOBIFLIGHT_AS1000_PFD_NAV_Small_INC,
    MOBIFLIGHT_AS1000_PFD_NAV_Small_DEC,
    MOBIFLIGHT_AS1000_PFD_NAV_Push,
    MOBIFLIGHT_AS1000_PFD_COM_Switch_Long,
    MOBIFLIGHT_AS1000_PFD_COM_Switch,
    MOBIFLIGHT_AS1000_PFD_COM_Large_INC,
    MOBIFLIGHT_AS1000_PFD_COM_Large_DEC,
    MOBIFLIGHT_AS1000_PFD_COM_Small_INC,
    MOBIFLIGHT_AS1000_PFD_COM_Small_DEC,
    MOBIFLIGHT_AS1000_PFD_COM_Push,
    MOBIFLIGHT_AS1000_PFD_BARO_INC,
    MOBIFLIGHT_AS1000_PFD_BARO_DEC,
    MOBIFLIGHT_AS1000_PFD_CRS_INC,
    MOBIFLIGHT_AS1000_PFD_CRS_DEC,
    MOBIFLIGHT_AS1000_PFD_CRS_PUSH,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_1,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_2,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_3,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_4,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_5,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_6,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_7,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_8,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_9,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Key10" })]
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_10,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_11,
    MOBIFLIGHT_AS1000_PFD_SOFTKEYS_12,
    MOBIFLIGHT_AS1000_PFD_DIRECTTO,
    MOBIFLIGHT_AS1000_PFD_ENT_Push,
    MOBIFLIGHT_AS1000_PFD_CLR_Long,
    MOBIFLIGHT_AS1000_PFD_CLR,
    [SimActionEvent]
    [TouchPortalActionMapping("G1000", new[] { "PFD", "Menu" })]
    MOBIFLIGHT_AS1000_PFD_MENU_Push,
    MOBIFLIGHT_AS1000_PFD_FPL_Push,
    MOBIFLIGHT_AS1000_PFD_PROC_Push,
    MOBIFLIGHT_AS1000_PFD_FMS_Upper_INC,
    MOBIFLIGHT_AS1000_PFD_FMS_Upper_DEC,
    MOBIFLIGHT_AS1000_PFD_FMS_Upper_PUSH,
    MOBIFLIGHT_AS1000_PFD_FMS_Lower_INC,
    MOBIFLIGHT_AS1000_PFD_FMS_Lower_DEC,
    MOBIFLIGHT_AS1000_PFD_RANGE_INC,
    MOBIFLIGHT_AS1000_PFD_RANGE_DEC,
    MOBIFLIGHT_AS1000_PFD_JOYSTICK_PUSH,
    MOBIFLIGHT_AS1000_PFD_ActivateMapCursor,
    MOBIFLIGHT_AS1000_PFD_DeactivateMapCursor,
    MOBIFLIGHT_AS1000_PFD_JOYSTICK_RIGHT,
    MOBIFLIGHT_AS1000_PFD_JOYSTICK_LEFT,
    MOBIFLIGHT_AS1000_PFD_JOYSTICK_UP,
    MOBIFLIGHT_AS1000_PFD_JOYSTICK_DOWN,

    #endregion

    #region MFD

    MOBIFLIGHT_AS1000_MFD_VOL_1_INC,
    MOBIFLIGHT_AS1000_MFD_VOL_1_DEC,
    MOBIFLIGHT_AS1000_MFD_VOL_2_INC,
    MOBIFLIGHT_AS1000_MFD_VOL_2_DEC,
    MOBIFLIGHT_AS1000_MFD_NAV_Switch,
    MOBIFLIGHT_AS1000_MFD_NAV_Large_INC,
    MOBIFLIGHT_AS1000_MFD_NAV_Large_DEC,
    MOBIFLIGHT_AS1000_MFD_NAV_Small_INC,
    MOBIFLIGHT_AS1000_MFD_NAV_Small_DEC,
    MOBIFLIGHT_AS1000_MFD_NAV_Push,
    MOBIFLIGHT_AS1000_MFD_COM_Switch_Long,
    MOBIFLIGHT_AS1000_MFD_COM_Switch,
    MOBIFLIGHT_AS1000_MFD_COM_Large_INC,
    MOBIFLIGHT_AS1000_MFD_COM_Large_DEC,
    MOBIFLIGHT_AS1000_MFD_COM_Small_INC,
    MOBIFLIGHT_AS1000_MFD_COM_Small_DEC,
    MOBIFLIGHT_AS1000_MFD_COM_Push,
    MOBIFLIGHT_AS1000_MFD_BARO_INC,
    MOBIFLIGHT_AS1000_MFD_BARO_DEC,
    MOBIFLIGHT_AS1000_MFD_CRS_INC,
    MOBIFLIGHT_AS1000_MFD_CRS_DEC,
    MOBIFLIGHT_AS1000_MFD_CRS_PUSH,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_1,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_2,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_3,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_4,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_5,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_6,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_7,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_8,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_9,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_10,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_11,
    MOBIFLIGHT_AS1000_MFD_SOFTKEYS_12,
    MOBIFLIGHT_AS1000_MFD_DIRECTTO,
    MOBIFLIGHT_AS1000_MFD_ENT_Push,
    MOBIFLIGHT_AS1000_MFD_CLR_Long,
    MOBIFLIGHT_AS1000_MFD_CLR,
    MOBIFLIGHT_AS1000_MFD_MENU_Push,
    MOBIFLIGHT_AS1000_MFD_FPL_Push,
    MOBIFLIGHT_AS1000_MFD_PROC_Push,
    MOBIFLIGHT_AS1000_MFD_FMS_Upper_INC,
    MOBIFLIGHT_AS1000_MFD_FMS_Upper_DEC,
    MOBIFLIGHT_AS1000_MFD_FMS_Upper_PUSH,
    MOBIFLIGHT_AS1000_MFD_FMS_Lower_INC,
    MOBIFLIGHT_AS1000_MFD_FMS_Lower_DEC,
    MOBIFLIGHT_AS1000_MFD_RANGE_INC,
    MOBIFLIGHT_AS1000_MFD_RANGE_DEC,
    MOBIFLIGHT_AS1000_MFD_JOYSTICK_PUSH,
    MOBIFLIGHT_AS1000_MFD_ActivateMapCursor,
    MOBIFLIGHT_AS1000_MFD_DeactivateMapCursor,
    MOBIFLIGHT_AS1000_MFD_JOYSTICK_RIGHT,
    MOBIFLIGHT_AS1000_MFD_JOYSTICK_LEFT,
    MOBIFLIGHT_AS1000_MFD_JOYSTICK_UP,
    MOBIFLIGHT_AS1000_MFD_JOYSTICK_DOWN,

    #endregion

    #region MID

    MOBIFLIGHT_AS1000_MID_COM_1_Push,
    MOBIFLIGHT_AS1000_MID_COM_2_Push,
    MOBIFLIGHT_AS1000_MID_COM_3_Push,
    MOBIFLIGHT_AS1000_MID_COM_Mic_1_Push,
    MOBIFLIGHT_AS1000_MID_COM_Mic_2_Push,
    MOBIFLIGHT_AS1000_MID_COM_Mic_3_Push,
    MOBIFLIGHT_AS1000_MID_COM_Swap_1_2_Push,
    MOBIFLIGHT_AS1000_MID_TEL_Push,
    MOBIFLIGHT_AS1000_MID_PA_Push,
    MOBIFLIGHT_AS1000_MID_SPKR_Push,
    MOBIFLIGHT_AS1000_MID_MKR_Mute_Push,
    MOBIFLIGHT_AS1000_MID_HI_SENS_Push,
    MOBIFLIGHT_AS1000_MID_DME_Push,
    MOBIFLIGHT_AS1000_MID_NAV_1_Push,
    MOBIFLIGHT_AS1000_MID_NAV_2_Push,
    MOBIFLIGHT_AS1000_MID_ADF_Push,
    MOBIFLIGHT_AS1000_MID_AUX_Push,
    MOBIFLIGHT_AS1000_MID_MAN_SQ_Push,
    MOBIFLIGHT_AS1000_MID_Play_Push,
    MOBIFLIGHT_AS1000_MID_Isolate_Pilot_Push,
    MOBIFLIGHT_AS1000_MID_Isolate_Copilot_Push,
    MOBIFLIGHT_AS1000_MID_Pass_Copilot_INC,
    MOBIFLIGHT_AS1000_MID_Pass_Copilot_DEC,
    MOBIFLIGHT_AS1000_MID_Display_Backup_Push,

    #endregion

    #endregion
  }
}
