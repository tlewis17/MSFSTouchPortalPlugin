using MSFSTouchPortalPlugin.Attributes;
using System;

namespace MSFSTouchPortalPlugin.Constants {
  public enum Definition {
    Init = 0,
    AileronTrimPct,
    AirSpeedIndicated,
    AirSpeedMach,
    AirSpeedTrue,
    AutoPilotAirSpeedHold,
    AutoPilotAirSpeedVar,
    AutoPilotAltitudeHold,
    AutoPilotAltitudeVar,
    AutoPilotApproachHold,
    AutoPilotAttitudeHold,
    AutoPilotAttitudeVar,
    AutoPilotAvailable,
    AutoPilotBackCourseHold,
    AutoPilotBanking,
    AutoPilotFlightDirector,
    AutoPilotFlightDirectorCurrentBank,
    AutoPilotFlightDirectorCurrentPitch,
    AutoPilotHeadingHold,
    AutoPilotHeadingVar,
    AutoPilotMach,
    AutoPilotMachVar,
    AutoPilotMaster,
    AutoPilotNav1Hold,
    AutoPilotNavSelected,
    AutoPilotPitchHold,
    AutoPilotVerticalSpeedHold,
    AutoPilotVerticalSpeedVar,
    AutoPilotWingLeveler,
    AutoPilotYawDampener,
    AutoThrottleArm,
    AutoThrottleGA,
    Com1ActiveFrequency,
    Com1StandbyFrequency,
    Com2ActiveFrequency,
    Com2StandbyFrequency,
    FlapSpeedExceeeded,
    GroundAltitude,
    GroundVelocity,
    LightBeaconOn,
    LightBrakeOn,
    LightCabinOn,
    LightHeadOn,
    LightLandingOn,
    LightLogoOn,
    LightNavOn,
    LightPanelOn,
    LightRecognitionOn,
    LightStrobeOn,
    LightTaxiOn,
    LightWingOn,
    MasterAlternator,
    MasterBattery,
    MasterIgnitionSwitch,
    MixtureEngine1,
    MixtureEngine2,
    MixtureEngine3,
    MixtureEngine4,
    Nav1ActiveFrequency,
    Nav1StandbyFrequency,
    Nav2ActiveFrequency,
    Nav2StandbyFrequency,
    OverspeedWarning,
    ParkingBrakeIndicator,
    PitotHeat,
    PlaneAltitude,
    PlaneAltitudeAGL,
    PlaneBankAngle,
    PlaneHeadingTrue,
    PlaneHeadingMagnetic,
    PlanePitchAngle,
    PropellerEngine1,
    PropellerEngine2,
    PropellerEngine3,
    PropellerEngine4,
    RPMN1Engine1,
    RPMN1Engine2,
    RPMN1Engine3,
    RPMN1Engine4,
    RPMPropeller1,
    RPMPropeller2,
    RPMPropeller3,
    RPMPropeller4,
    RudderTrimPct,
    SimulationRate,
    StallWarning,
    ThrottleEngine1,
    ThrottleEngine2,
    ThrottleEngine3,
    ThrottleEngine4,
    VerticalSpeed,

    #region Landing Gear
    GearTotalExtended,
    #endregion

    #region Trimming
    AileronTrim,
    ElevatorTrim,
    RudderTrim,
    #endregion
  }

  public enum Request {
    Init = 0,
    AileronTrimPct,
    AirSpeedIndicated,
    AirSpeedMach,
    AirSpeedTrue,
    AutoPilotAirSpeedHold,
    AutoPilotAirSpeedVar,
    AutoPilotAltitudeHold,
    AutoPilotAltitudeVar,
    AutoPilotApproachHold,
    AutoPilotAttitudeHold,
    AutoPilotAttitudeVar,
    AutoPilotAvailable,
    AutoPilotBackCourseHold,
    AutoPilotBanking,
    AutoPilotFlightDirector,
    AutoPilotFlightDirectorCurrentBank,
    AutoPilotFlightDirectorCurrentPitch,
    AutoPilotHeadingHold,
    AutoPilotHeadingVar,
    AutoPilotMach,
    AutoPilotMachVar,
    AutoPilotMaster,
    AutoPilotNav1Hold,
    AutoPilotNavSelected,
    AutoPilotPitchHold,
    AutoPilotVerticalSpeedHold,
    AutoPilotVerticalSpeedVar,
    AutoPilotWingLeveler,
    AutoPilotYawDampener,
    AutoThrottleArm,
    AutoThrottleGA,
    Com1ActiveFrequency,
    Com1StandbyFrequency,
    Com2ActiveFrequency,
    Com2StandbyFrequency,
    FlapSpeedExceeeded,
    GroundAltitude,
    GroundVelocity,
    LightBeaconOn,
    LightBrakeOn,
    LightCabinOn,
    LightHeadOn,
    LightLandingOn,
    LightLogoOn,
    LightNavOn,
    LightPanelOn,
    LightRecognitionOn,
    LightStrobeOn,
    LightTaxiOn,
    LightWingOn,
    MasterAlternator,
    MasterBattery,
    MasterIgnitionSwitch,
    MixtureEngine1,
    MixtureEngine2,
    MixtureEngine3,
    MixtureEngine4,
    Nav1ActiveFrequency,
    Nav1StandbyFrequency,
    Nav2ActiveFrequency,
    Nav2StandbyFrequency,
    OverspeedWarning,
    ParkingBrakeIndicator,
    PitotHeat,
    PlaneAltitude,
    PlaneAltitudeAGL,
    PlaneBankAngle,
    PlaneHeadingTrue,
    PlaneHeadingMagnetic,
    PlanePitchAngle,
    PropellerEngine1,
    PropellerEngine2,
    PropellerEngine3,
    PropellerEngine4,
    RPMN1Engine1,
    RPMN1Engine2,
    RPMN1Engine3,
    RPMN1Engine4,
    RPMPropeller1,
    RPMPropeller2,
    RPMPropeller3,
    RPMPropeller4,
    RudderTrimPct,
    SimulationRate,
    StallWarning,
    ThrottleEngine1,
    ThrottleEngine2,
    ThrottleEngine3,
    ThrottleEngine4,
    VerticalSpeed,

    #region Landing Gear
    GearTotalExtended,
    #endregion

    #region Trimming
    AileronTrim,
    ElevatorTrim,
    RudderTrim,
    #endregion
  }

  public static class SimVars {

    #region Engine Systems

    #endregion

    #region Flight Systems


    #endregion

    #region Fuel Systems
    #endregion

    #region Lights

    #endregion

    #region Measurements



    #endregion

  }

  public class SimVarItem {
    public Definition def;
    public Request req;
    public string SimVarName;
    public string Unit;
    public bool CanSet = false;
    public bool PendingRequest = false;
    public DateTime LastPending = DateTime.Now;
    public string TouchPortalStateMapping;
    public string Value = string.Empty;
    public string StringFormat = "{0}";
    public string TouchPortalStateId = "";

    public void SetPending(bool val) {
      PendingRequest = val;

      if (val) {
        LastPending = DateTime.Now;
      }
    }

    /// <summary>
    /// If pending for more than 30 seconds, timeout
    /// </summary>
    public void PendingTimeout() {
      if (PendingRequest) {
        if (DateTime.Now > LastPending.AddSeconds(30)) {
          SetPending(false);
        }
      }
    }
  }
}
