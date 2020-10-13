using MSFSTouchPortalPlugin.Attributes;
using MSFSTouchPortalPlugin.Constants;
using MSFSTouchPortalPlugin.Enums;
using TouchPortalExtension.Attributes;

namespace MSFSTouchPortalPlugin.Objects.InstrumentsSystems {
  [SimVarDataRequestGroup]
  [TouchPortalCategory("FlightInstruments", "MSFS - Flight Instruments")]
  internal class FlightInstrumentsMapping {

    #region Velocity

    [SimVarDataRequest]
    [TouchPortalState("GroundVelocity", "text", "Ground Speed in Knots", "")]
    public static SimVarItem GroundVelocity = new SimVarItem { def = Definition.GroundVelocity, req = Request.GroundVelocity, SimVarName = "GROUND VELOCITY", Unit = Units.knots, CanSet = false, StringFormat = "{0:0.0#}" };

    [SimVarDataRequest]
    [TouchPortalState("AirSpeedTrue", "text", "Air speed true in Knots", "")]
    public static SimVarItem AirSpeedTrue = new SimVarItem { def = Definition.AirSpeedTrue, req = Request.AirSpeedTrue, SimVarName = "AIRSPEED TRUE", Unit = Units.knots, CanSet = true, StringFormat = "{0:0.0#}" };

    [SimVarDataRequest]
    [TouchPortalState("AirSpeedIndicated", "text", "Air speed indicated in Knots", "")]
    public static SimVarItem AirSpeedIndicated = new SimVarItem { def = Definition.AirSpeedIndicated, req = Request.AirSpeedIndicated, SimVarName = "AIRSPEED INDICATED", Unit = Units.knots, CanSet = true, StringFormat = "{0:0.0#}" };

    [SimVarDataRequest]
    [TouchPortalState("AirSpeedMach", "text", "Air speed indicated in mach", "")]
    public static SimVarItem AirSpeedMach = new SimVarItem { def = Definition.AirSpeedMach, req = Request.AirSpeedMach, SimVarName = "AIRSPEED MACH", Unit = Units.mach, CanSet = true, StringFormat = "{0:0.0#}" };
    #endregion

    #region Altitude

    [SimVarDataRequest]
    [TouchPortalState("PlaneAltitude", "text", "Plane Altitude in Feet", "")]
    public static SimVarItem PlaneAltitude = new SimVarItem { def = Definition.PlaneAltitude, req = Request.PlaneAltitude, SimVarName = "PLANE ALTITUDE", Unit = Units.feet, CanSet = false, StringFormat = "{0:0.#}" };

    [SimVarDataRequest]
    [TouchPortalState("PlaneAltitudeAGL", "text", "Plane Altitude AGL in Feet", "")]
    public static SimVarItem PlaneAltitudeAGL = new SimVarItem { def = Definition.PlaneAltitudeAGL, req = Request.PlaneAltitudeAGL, SimVarName = "PLANE ALT ABOVE GROUND", Unit = Units.feet, CanSet = false, StringFormat = "{0:0.#}" };

    [SimVarDataRequest]
    [TouchPortalState("GroundAltitude", "text", "Ground level in Feet", "")]
    public static SimVarItem GroundAltitude = new SimVarItem { def = Definition.GroundAltitude, req = Request.GroundAltitude, SimVarName = "GROUND ALTITUDE", Unit = Units.feet, CanSet = false, StringFormat = "{0:0.#}" };

    #endregion

    #region Heading

    [SimVarDataRequest]
    [TouchPortalState("PlaneHeadingTrue", "text", "Plane Heading (True North) in Degrees", "")]
    public static SimVarItem PlaneHeadingTrue = new SimVarItem { def = Definition.PlaneHeadingTrue, req = Request.PlaneHeadingTrue, SimVarName = "PLANE HEADING DEGREES TRUE", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    [SimVarDataRequest]
    [TouchPortalState("PlaneHeadingMagnetic", "text", "Plane Heading (Magnetic North) in Degrees", "")]
    public static SimVarItem PlaneHeadingMagnetic = new SimVarItem { def = Definition.PlaneHeadingMagnetic, req = Request.PlaneHeadingMagnetic, SimVarName = "PLANE HEADING DEGREES MAGNETIC", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    #endregion

    #region Bank and Pitch

    [SimVarDataRequest]
    [TouchPortalState("PlaneBankAngle", "text", "Plane Bank Angle in Degrees", "")]
    public static SimVarItem PlaneBankAngle = new SimVarItem { def = Definition.PlaneBankAngle, req = Request.PlaneBankAngle, SimVarName = "PLANE BANK DEGREES", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    [SimVarDataRequest]
    [TouchPortalState("PlanePitchAngle", "text", "Plane Pitch Angle in Degrees", "")]
    public static SimVarItem PlanePitchAngle = new SimVarItem { def = Definition.PlanePitchAngle, req = Request.PlanePitchAngle, SimVarName = "PLANE PITCH DEGREES", Unit = Units.radians, CanSet = false, StringFormat = "{0:0}" };

    [SimVarDataRequest]
    [TouchPortalState("VerticalSpeed", "text", "Vertical Speed in feet per minute", "")]
    public static SimVarItem VerticalSpeed = new SimVarItem { def = Definition.VerticalSpeed, req = Request.VerticalSpeed, SimVarName = "VERTICAL SPEED", Unit = Units.feetminute, CanSet = true, StringFormat = "{0:0.0#}" };

      #endregion

      #region Warnings

    [SimVarDataRequest]
    [TouchPortalState("StallWarning", "text", "Stall Warning true/false", "")]
    public static SimVarItem StallWarning = new SimVarItem { def = Definition.StallWarning, req = Request.StallWarning, SimVarName = "STALL WARNING", Unit = Units.Bool, CanSet = false };

    [SimVarDataRequest]
    [TouchPortalState("OverspeedWarning", "text", "Overspeed Warning true/false", "")]
    public static SimVarItem OverspeedWarning = new SimVarItem { def = Definition.OverspeedWarning, req = Request.OverspeedWarning, SimVarName = "OVERSPEED WARNING", Unit = Units.Bool, CanSet = false };

    [SimVarDataRequest]
    [TouchPortalState("FlapSpeedExceeeded", "text", "Flap Speed Exceeded Warning true/false", "")]
    public static SimVarItem FlapSpeedExceeeded = new SimVarItem { def = Definition.FlapSpeedExceeeded, req = Request.FlapSpeedExceeeded, SimVarName = "FLAP SPEED EXCEEDED", Unit = Units.Bool, CanSet = false };

    #endregion

  }

  [SimNotificationGroup(Groups.FlightInstruments)]
  [TouchPortalCategoryMapping("FlightInstruments")]
  internal enum FlightInstruments {
    // Placeholder to offset each enum for SimConnect
    Init = 7000,
  }
}
