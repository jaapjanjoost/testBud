using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NavBuddy;

public static class BuddyPilot
{
	public enum FlightPhase
	{
		TakeOff,
		Navigation,
		Approach,
		Landing
	}

	private static double oldPLANE_HEADING_DEGREES_TRUE;

	private static double oldPLANE_BANK_DEGREES;

	private static double oldINCIDENCE_ALPHA;

	private static double oldVerticalSpeedFeetPerSecond;

	private static double oldIAS;

	private static double oldPitchRadiant;

	private static double rudder;

	private static double ailerons;

	private static double elevator;

	private static double autoThrottle;

	public static double WindCompensationAngleDegree = 0.0;

	public static double targetGeoHeadingDegree = 300.0;

	public static StringBuilder BuddyPilotLogBuilder;

	private static FlightPhase currentPhase;

	private static int indexSogliaPista = -1;

	public static void AiPilotProcedure(SimulatorConnectionManager.Struct2 data, FormMain FM, FlightPanelForm fc)
	{
		if (fc.btnAIPilot.Tag != null && (bool)fc.btnAIPilot.Tag && SimulatorConnectionManager.MySim != null)
		{
			rudder = data.RUDDER_POSITION;
			ailerons = data.AILERON_POSITION;
			elevator = data.ELEVATOR_POSITION;
			double num = 1000.0 / (double)FM.TimerQuick.Interval;
			WayPoint wayPoint = new WayPoint(data.latitude, data.longitude, "CURRENT POSITION", "AIRPLANE", data.altitude);
			ATCWaypoint aTCWaypoint = FlightPlan.ATCWaypoints[indexSogliaPista];
			double num2 = wayPoint.DistanceFromMiles(aTCWaypoint);
			double num3 = wayPoint.CurrentBearingDegree(aTCWaypoint);
			double value = Utility.MinimizeAngleDegree(num3 - Utility.RadToDeg(data.PLANE_HEADING_RADIANT_TRUE));
			bool flag = Math.Abs(value) > 90.0;
			double elapsedMiles = SimulatorInformationProcessing.elapsedMiles;
			double num4 = aTCWaypoint.Dist_sum_nm - SimulatorInformationProcessing.elapsedMiles;
			if (elapsedMiles < num4 && data.PLANE_ALT_ABOVE_GROUND < (double)FM.nudTakeOffCompletedAGL.Value && FM.radVnavStandard.Checked)
			{
				currentPhase = FlightPhase.TakeOff;
			}
			else if (SimulatorInformationProcessing.nextWaypointIndex >= indexSogliaPista)
			{
				if (flag && data.PLANE_ALT_ABOVE_GROUND < 200.0)
				{
					currentPhase = FlightPhase.Landing;
				}
				else
				{
					currentPhase = FlightPhase.Approach;
				}
			}
			else
			{
				currentPhase = FlightPhase.Navigation;
			}
			fc.labAI.Text = "AI: " + currentPhase;
			double rad = Utility.MinimizeAngleRad(data.PLANE_HEADING_RADIANT_TRUE - oldPLANE_HEADING_DEGREES_TRUE) * num;
			oldPLANE_HEADING_DEGREES_TRUE = data.PLANE_HEADING_RADIANT_TRUE;
			double num5 = Utility.MinimizeAngleRad(data.PLANE_BANK_RADIANT - oldPLANE_BANK_DEGREES) * num;
			oldPLANE_BANK_DEGREES = data.PLANE_BANK_RADIANT;
			double num6 = Utility.MinimizeAngleDegree(data.INCIDENCE_ALPHA - oldINCIDENCE_ALPHA) * num;
			oldINCIDENCE_ALPHA = data.INCIDENCE_ALPHA;
			double num7 = Utility.MinimizeAngleRad(data.PLANE_PITCH_RADIANT - oldPitchRadiant) * num;
			oldPitchRadiant = data.PLANE_PITCH_RADIANT;
			double num8 = (data.VERTICAL_SPEED - oldVerticalSpeedFeetPerSecond) * num;
			oldVerticalSpeedFeetPerSecond = data.VERTICAL_SPEED;
			double num9 = Utility.RadToDeg(Math.Atan2(data.VERTICAL_SPEED * 0.3048, data.GROUND_VELOCITY * 1852.0 / 3600.0));
			if (data.autopilotmaster)
			{
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.AP_MASTER, 1, 1f);
			}
			fc.labAISystem.Text = "SYSTEMS: ";
			double num10 = 0.0;
			double num11 = (double)FM.nudFlapsTakeOffPerc.Value;
			double num12 = (double)FM.nudFlapsLandingPerc.Value;
			double num13 = (double)FM.nudFlapsTakeOffIas.Value;
			double num14 = (double)FM.nudFlapsLandingIas.Value;
			bool flag2 = true;
			switch (currentPhase)
			{
			case FlightPhase.Approach:
			case FlightPhase.Landing:
				num10 = num12 * Utility.Clamp(1.0 - (data.AIRSPEED_INDICATED - (double)FM.nudSafeIas.Value) / (num14 - (double)FM.nudSafeIas.Value), 0.0, 1.0);
				flag2 = false;
				break;
			case FlightPhase.TakeOff:
				num10 = num11 * Utility.Clamp(1.0 - (data.AIRSPEED_INDICATED - (double)FM.nudLandingIas.Value) / (num13 - (double)FM.nudLandingIas.Value), 0.0, 1.0);
				break;
			default:
				num10 = 0.0;
				break;
			}
			if ((double)data.FLAPS_HANDLE_PERCENT < num10 - 1.0)
			{
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.FLAPS_INCR, 1, 0.2f);
			}
			if (((double)data.FLAPS_HANDLE_PERCENT > num10 + 1.0) & flag2)
			{
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.FLAPS_DECR, 1, 0.2f);
			}
			Label labAISystem = fc.labAISystem;
			labAISystem.Text = labAISystem.Text + "FLAPS: " + num10.ToString("F0") + "% / " + data.FLAPS_HANDLE_PERCENT.ToString("F0") + "%";
			if (currentPhase == FlightPhase.Landing && data.GROUND_VELOCITY < 1.0)
			{
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.ENGINE_AUTO_SHUTDOWN, 1, 0.1f);
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.MASTER_BATTERY_OFF, 1, 0.1f);
			}
			bool flag3 = currentPhase == FlightPhase.Landing && data.GROUND_VELOCITY < 3.0;
			if (data.BRAKE_PARKING_INDICATOR != flag3)
			{
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.PARKING_BRAKES, 1, 0.2f);
			}
			if (flag3)
			{
				fc.labAISystem.Text += "BRAKE ";
			}
			bool flag4 = true;
			switch (currentPhase)
			{
			case FlightPhase.Approach:
			case FlightPhase.Landing:
				flag4 = ((data.PLANE_ALT_ABOVE_GROUND <= (double)FM.nudLandingGearDownAGL.Value) ? true : false);
				break;
			case FlightPhase.TakeOff:
				flag4 = !(data.PLANE_ALT_ABOVE_GROUND >= (double)FM.nudLandingGearUpAGL.Value);
				break;
			default:
				flag4 = false;
				break;
			}
			if (data.GEAR_HANDLE_POSITION != flag4)
			{
				SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.GEAR_TOGGLE, 1, 0.1f);
			}
			if (flag4)
			{
				fc.labAISystem.Text += "GEAR ";
			}
			if (currentPhase == FlightPhase.TakeOff)
			{
				double num15 = FlightPlan.TakeOffRunway.runwayThreshold.CurrentBearingDegree(wayPoint);
				double value2 = wayPoint.DistanceFromMiles(FlightPlan.TakeOffRunway.runwayThreshold) * 5.0 * Utility.MinimizeAngleDegree(FlightPlan.TakeOffRunway.BearingDegreeTrue() - num15);
				value2 = Utility.Clamp(value2, -5.0, 5.0);
				targetGeoHeadingDegree = Utility.NormalizeAngleDegree(FlightPlan.TakeOffRunway.BearingDegreeTrue() + value2);
				fc.labAI_LNAV.Text = "LNAV: TAKE OFF ";
			}
			else if (SimulatorInformationProcessing.nextWaypointIndex > indexSogliaPista)
			{
				double num16 = wayPoint.CurrentBearingDegree(FlightPlan.LandingRunway.runwayTerminal);
				double num17 = 0.0;
				double num18 = 5.0 * wayPoint.DistanceFromMiles(FlightPlan.LandingRunway.runwayTerminal);
				double num19 = Utility.MinimizeAngleDegree(FlightPlan.LandingRunway.BearingDegreeTrue() - num16);
				num17 = (0.0 - num18) * num19;
				num17 = Math.Min(45.0, num17);
				num17 = Math.Max(-45.0, num17);
				targetGeoHeadingDegree = num16 + num17;
				fc.labAI_LNAV.Text = "LNAV: RUNWAY ALIGNMENT ";
			}
			else if (SimulatorInformationProcessing.nextWaypoint != null)
			{
				double num20 = wayPoint.CurrentBearingDegree(SimulatorInformationProcessing.nextWaypoint);
				double num21 = 0.0;
				double num22 = 0.0;
				if (SimulatorInformationProcessing.nextWaypoint.preceeding != null)
				{
					double num23 = SimulatorInformationProcessing.nextWaypoint.preceeding.CurrentBearingDegree(SimulatorInformationProcessing.nextWaypoint);
					double num24 = 3.0 * (100.0 / Math.Max(data.GROUND_VELOCITY, 70.0)) * Math.Min(wayPoint.DistanceFromMiles(SimulatorInformationProcessing.nextWaypoint), 1.0);
					num22 = Utility.MinimizeAngleDegree(num23 - num20);
					num21 = (0.0 - num24) * num22;
					num21 = Math.Min(45.0, num21);
					num21 = Math.Max(-45.0, num21);
				}
				targetGeoHeadingDegree = num20 + num21;
				fc.labAI_LNAV.Text = "LNAV: " + SimulatorInformationProcessing.nextWaypoint.Id;
			}
			else
			{
				fc.labAI_LNAV.Text = "LNAV: ERROR ";
			}
			double y = data.AMBIENT_WIND_VELOCITY * Math.Sin(Utility.DegToRad(Utility.MinimizeAngleDegree(data.AMBIENT_WIND_DIRECTION - targetGeoHeadingDegree)));
			WindCompensationAngleDegree = 0.0;
			if (data.PLANE_ALT_ABOVE_GROUND > 15.0)
			{
				WindCompensationAngleDegree = Utility.Clamp(data.PLANE_ALT_ABOVE_GROUND / 25.0, 0.0, 1.0) * Utility.RadToDeg(Math.Atan2(y, data.GROUND_VELOCITY));
			}
			targetGeoHeadingDegree += WindCompensationAngleDegree;
			targetGeoHeadingDegree = Utility.NormalizeAngleDegree(targetGeoHeadingDegree);
			double num25 = 0.0;
			if (currentPhase == FlightPhase.TakeOff)
			{
				num25 = (double)FM.nudClimbFPM.Value;
				fc.labAI_VNAV.Text = "VNAV: TAKE OFF ";
			}
			else if (currentPhase == FlightPhase.Approach)
			{
				double num26 = (double)(int)FM.nudRunwayEntAGL.Value - data.altitude + (double)aTCWaypoint.Altitude;
				double num27 = 60.0 * num2 / data.GROUND_VELOCITY;
				double num28 = num27 * 60.0;
				double num29 = 5.0;
				fc.labAI_VNAV.Text = "VNAV: APPROACH ";
				if (num28 < num29)
				{
					double num30 = (num29 - num28) / num29;
					num25 = num26 / num27 * (1.0 - num30) + data.VERTICAL_SPEED * 60.0 * num30;
					fc.labAI_VNAV.Text += "STABILIZATION!";
				}
				else
				{
					double num31 = Utility.RadToDeg(Math.Atan2((0.0 - num26) / 6076.12, num2));
					double num32 = Utility.RadToDeg(Math.Atan2((double)(aTCWaypoint.preceeding.Altitude - aTCWaypoint.Altitude) / 6076.12, aTCWaypoint.Dist_nm));
					num25 = num26 / num27 * (num31 / num32);
					labAISystem = fc.labAI_VNAV;
					labAISystem.Text = labAISystem.Text + "SLOPE: " + num31.ToString("F1") + "°/" + num32.ToString("F1") + "°";
					double num33 = data.PLANE_ALT_ABOVE_GROUND - (data.altitude - (double)aTCWaypoint.Altitude);
					Label labAI_VNAV = fc.labAI_VNAV;
					labAI_VNAV.Text = labAI_VNAV.Text + " [ TGAP: " + num33.ToString("F0") + "ft ] ";
				}
			}
			else if (currentPhase == FlightPhase.Landing)
			{
				num25 = (double)FM.nudLandFPM.Value;
				fc.labAI_VNAV.Text = "VNAV: FLARE ";
			}
			else if (FM.radVnavHoldAGL.Checked)
			{
				double num34 = (double)FM.nudVnavHoldAlt.Value - data.PLANE_ALT_ABOVE_GROUND;
				SimulatorInformationProcessing.altitudeGoal = (int)((double)FM.nudVnavHoldAlt.Value + (data.INDICATED_ALTITUDE - data.PLANE_ALT_ABOVE_GROUND));
				num25 = Utility.Clamp(num34 * 5.0, (double)FM.nudDescFPM.Value, (double)FM.nudClimbFPM.Value);
				fc.labAI_VNAV.Text = "VNAV: HOLD AGL ";
			}
			else
			{
				fc.labAI_VNAV.Text = "VNAV: AUTO ";
				num25 = SimulatorInformationProcessing.fpmIdeal;
			}
			if (num25 > 0.0)
			{
				fc.labAI_VNAV.Text += " FLC ";
				if (FM.nudClimbIas.Value - FM.nudSafeIas.Value < 1m)
				{
					FM.nudClimbIas.Value = FM.nudSafeIas.Value + 10m;
				}
				double value3 = (data.AIRSPEED_INDICATED - (double)FM.nudSafeIas.Value) / ((double)FM.nudClimbIas.Value - (double)FM.nudSafeIas.Value);
				value3 = Utility.Clamp(value3, 0.0, 1.0);
				num25 = value3 * num25;
			}
			if (num25 < (double)FM.nudDescFPM.Value * 1.5)
			{
				fc.labAI_VNAV.Text = " ! STRESS ! ";
				num25 = (double)FM.nudDescFPM.Value * 1.5;
			}
			double num35 = 0.0;
			string text = "";
			if (currentPhase == FlightPhase.TakeOff)
			{
				num35 = (double)FM.nudClimbIas.Value;
				text = " CLIMB/T.O.";
			}
			else if (currentPhase == FlightPhase.Approach)
			{
				num35 = (double)FM.nudLandingIas.Value;
				text = " LANDING";
			}
			else if (currentPhase == FlightPhase.Landing)
			{
				num35 = 0.0;
				text = " STOP!";
			}
			else
			{
				if (num25 > 100.0 && Math.Abs(data.INDICATED_ALTITUDE - (double)FM.nudPlannedCruiseAltitude.Value) > 500.0)
				{
					num35 = (double)FM.nudClimbIas.Value;
					text = " CLIMB";
				}
				else if (num25 < -100.0 && Math.Abs(data.INDICATED_ALTITUDE - (double)FM.nudPlannedCruiseAltitude.Value) > 500.0)
				{
					num35 = (double)FM.nudDescIas.Value;
					text = " DESC";
				}
				else
				{
					num35 = (double)FM.nudCruiseIas.Value;
					text = " CRUISE";
				}
				if (SimulatorInformationProcessing.TimeToTOD.TotalSeconds < (double)((FM.nudCruiseIas.Value - FM.nudDescIas.Value) / 2m))
				{
					num35 = (double)FM.nudDescIas.Value;
					text += " SLOWING FOR DESC";
				}
				if (SimulatorInformationProcessing.nextWaypointIndex + 1 == indexSogliaPista && SimulatorInformationProcessing.nextWaypoint.arrival_time_expected - SimulatorInformationProcessing.SimZulu_Time < new TimeSpan(0, 0, 10))
				{
					num35 = (double)FM.nudLandingIas.Value;
					text += " SLOWING FOR APPR";
				}
			}
			double num36 = (data.AIRSPEED_INDICATED - oldIAS) * num;
			double num37 = num35 - data.AIRSPEED_INDICATED;
			if (currentPhase == FlightPhase.Landing)
			{
				if (data.VERTICAL_SPEED * 60.0 > (double)FM.nudLandFPM.Value)
				{
					autoThrottle -= 5.0 / num;
					double min = Math.Max((double)FM.nudReverseThrust.Value, 0.0 - data.GROUND_VELOCITY);
					autoThrottle = Utility.Clamp(autoThrottle, min, 100.0);
				}
				fc.labAISpeed.Text = "LANDING! THRT: " + autoThrottle.ToString("F0") + "% " + text;
			}
			else
			{
				oldIAS = data.AIRSPEED_INDICATED;
				autoThrottle += 0.03 * ((double)FM.nudThrottleEffect.Value / 100.0) * num37 - 0.2 * ((double)FM.nudThrottleDamper.Value / 100.0) * num36;
				autoThrottle = Math.Min(autoThrottle, 100.0);
				autoThrottle = Math.Max(autoThrottle, 0.0);
				fc.labAISpeed.Text = "IAS: " + num35 + " THRT: " + autoThrottle.ToString("F0") + "% " + text;
				autoThrottle = Utility.Clamp(autoThrottle, 0.0, 100.0);
			}
			SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT5, new SimulatorConnectionManager.Struct5
			{
				GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = autoThrottle,
				GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = autoThrottle,
				GENERAL_ENG_THROTTLE_LEVER_POSITION_3 = autoThrottle,
				GENERAL_ENG_THROTTLE_LEVER_POSITION_4 = autoThrottle
			});
			double sPOILERS_HANDLE_POSITION = 0.0;
			if (currentPhase != FlightPhase.Landing && num25 < 0.0 && num37 < 0.0)
			{
				sPOILERS_HANDLE_POSITION = 100.0 * Utility.Clamp(2.0 * ((0.0 - num37) / num35) + 0.05 * num36, 0.0, 1.0);
			}
			SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT9, new SimulatorConnectionManager.Struct9
			{
				SPOILERS_HANDLE_POSITION = sPOILERS_HANDLE_POSITION
			});
			if (currentPhase == FlightPhase.Landing)
			{
				double num38 = Utility.Clamp(105.0 - 15.0 * Utility.RadToDeg(data.PLANE_PITCH_RADIANT), 0.0, 100.0);
				SimulatorInformationProcessing.SetDifferentialBrakes(num38, num38);
				Label labAISpeed = fc.labAISpeed;
				labAISpeed.Text = labAISpeed.Text + " BRK: " + num38.ToString("F0") + "%";
			}
			else
			{
				SimulatorInformationProcessing.SetDifferentialBrakes(0.0, 0.0);
			}
			double deg = -1.0 * Utility.Clamp(Utility.MinimizeAngleDegree(targetGeoHeadingDegree - Utility.RadToDeg(data.PLANE_HEADING_RADIANT_TRUE)), 0.0 - (double)FM.nudMaxBankAngle.Value, (double)FM.nudMaxBankAngle.Value) - 0.5 * Utility.RadToDeg(rad);
			double num39 = Utility.DegToRad(deg);
			double value4 = Utility.Clamp(0.75 * ((double)FM.nudAileronEffect.Value * 0.01) * (data.PLANE_BANK_RADIANT - num39) + 0.01 * ((double)FM.nudAileronDamper.Value * 0.01) * num5, -1.0, 1.0);
			ailerons = Utility.Clamp(value4, ailerons - 1.0 / num, ailerons + 1.0 / num);
			ailerons = Utility.Clamp(ailerons, -1.0, 1.0);
			labAISystem = fc.labAI_LNAV;
			labAISystem.Text = labAISystem.Text + " " + Utility.RadToDeg(data.PLANE_HEADING_RADIANT_TRUE).ToString("F0") + "°/" + targetGeoHeadingDegree.ToString("F0") + "° [" + WindCompensationAngleDegree.ToString("F0") + "°]";
			labAISystem = fc.labAI_LNAV;
			labAISystem.Text = labAISystem.Text + " BNK=" + Utility.RadToDeg(data.PLANE_BANK_RADIANT).ToString("F0") + "°/" + deg.ToString("F0") + "°";
			if (currentPhase == FlightPhase.TakeOff || currentPhase == FlightPhase.Landing || currentPhase == FlightPhase.Approach)
			{
				double num40 = 0.0;
				num40 = targetGeoHeadingDegree;
				double num41 = 500.0;
				double num42 = 0.0;
				if (data.PLANE_ALT_ABOVE_GROUND < num41)
				{
					num42 = 1.0 - data.PLANE_ALT_ABOVE_GROUND / num41;
				}
				double num43 = num42 * 0.001 * (double)FM.nudRudderEffect.Value;
				double num44 = 0.0;
				rudder = Utility.Clamp((0.0 - num44) * Utility.RadToDeg(rad) + num43 * Utility.MinimizeAngleDegree(num40 - Utility.RadToDeg(data.PLANE_HEADING_RADIANT_TRUE)), -1.0, 1.0);
				Label labAI_LNAV = fc.labAI_LNAV;
				labAI_LNAV.Text = labAI_LNAV.Text + " RDR: /" + num40.ToString("F0") + "°";
			}
			else
			{
				rudder = 0.0;
			}
			double num45 = (double)FM.nudElevatorEffect.Value / 100.0;
			double num46 = (double)FM.nudElevatorDamper.Value / 100.0;
			double num47 = 2.5E-06 * Utility.Clamp(num25 - data.VERTICAL_SPEED * 60.0, -800.0, 800.0) * num45;
			double num48 = -0.0009 * num8 * num46;
			elevator += num47 + num48;
			elevator = Utility.Clamp(elevator, -0.75, 0.75);
			labAISystem = fc.labAI_VNAV;
			labAISystem.Text = labAISystem.Text + " " + (data.VERTICAL_SPEED * 60.0).ToString("F0") + "/" + num25.ToString("F0") + " fpm";
			SimulatorInformationProcessing.SetSurfaceControls(ailerons, elevator, rudder);
			if (BuddyPilotLogBuilder != null)
			{
				BuddyPilotLogBuilder.AppendLine(data.AIRSPEED_INDICATED.ToString("F0") + "," + num35.ToString("F0") + "," + (data.VERTICAL_SPEED * 60.0).ToString("F0") + "," + num25.ToString("F0") + "," + targetGeoHeadingDegree.ToString("F0") + "," + Utility.RadToDeg(data.PLANE_HEADING_RADIANT_TRUE).ToString("F0") + "," + (100.0 * ailerons).ToString("F0") + "," + (100.0 * elevator).ToString("F0") + "," + (100.0 * rudder).ToString("F0") + ",");
			}
		}
		else
		{
			fc.labAI.Text = "AI: Inoperative";
			fc.labAISystem.Text = "/";
			fc.labAISpeed.Text = "/";
			fc.labAI_LNAV.Text = "/";
			fc.labAI_VNAV.Text = "/";
		}
	}

	public static bool CheckPrecondition()
	{
		if (FlightPlan.LandingRunway == null || FlightPlan.TakeOffRunway == null)
		{
			MessageBox.Show("Missing Takeoff and/or Landing runway");
			return false;
		}
		double num = double.MaxValue;
		int index = -1;
		for (int i = 0; i < FlightPlan.ATCWaypoints.Count - 1; i++)
		{
			double num2 = FlightPlan.ATCWaypoints[i].DistanceFromMiles(FlightPlan.LandingRunway.runwayThreshold);
			if (num2 < num)
			{
				index = i;
				num = num2;
			}
		}
		if (num < 0.05)
		{
			indexSogliaPista = index;
			FlightPlan.ATCWaypoints[index].Altitude = FlightPlan.LandingRunway.runwayThreshold.Altitude;
			FlightPlan.ATCWaypoints[index].latitude = FlightPlan.LandingRunway.runwayThreshold.latitude;
			FlightPlan.ATCWaypoints[index].longitude = FlightPlan.LandingRunway.runwayThreshold.longitude;
			return true;
		}
		MessageBox.Show("Missing runway threshold waypoint. The plan is not acceptable for Navbuddy Pilot.");
		return false;
	}

	public static void ActivationProcedure()
	{
		BuddyPilotLogBuilder = new StringBuilder();
	}

	public static void DeactivationProcedure()
	{
		string path = DataManager.DataFolder() + "\\BuddyPilotLog" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
		string value = "IAS,tgtIAS,VS,tgtVS,HDG,tgtHDG,aileron,elevator,rudder\r\n";
		BuddyPilotLogBuilder.Insert(0, value);
		File.AppendAllText(path, BuddyPilotLogBuilder.ToString());
	}
}
