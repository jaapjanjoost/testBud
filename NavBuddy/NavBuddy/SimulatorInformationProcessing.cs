using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;
using NavBuddy.BuddyWorld;

namespace NavBuddy;

public static class SimulatorInformationProcessing
{
	public static List<WayPoint> FligthTrackRecord = new List<WayPoint>();

	public static int lastInvalidatedRow;

	public static Aircraft currentAircraft = new Aircraft();

	public static FormMain F = null;

	public static SimulatorConnectionManager.Struct1 lastBigInfoSimulatorData;

	public static SimulatorConnectionManager.Struct2 lastSmallInfoSimulatorData;

	public static SimulatorConnectionManager.Struct10 lastAirplaneinfoSimulatorData;

	public static DateTime SimLocal_Time;

	public static DateTime SimZulu_Time;

	public static DateTime ZuluSimulationTime_10secAgo = DateTime.MinValue;

	public static double fuelQuantity_10secAgo = 0.0;

	public static double elapsedMiles = 0.0;

	public const double WAYPOINT_INTERCEPTION_DISTANCE = 0.5;

	public static ATCWaypoint nextWaypoint;

	public static int nextWaypointIndex;

	public static double fpmIdeal = 0.0;

	public static int altitudeGoal = 0;

	public static double deltafeet = 0.0;

	public static TimeSpan TimeToTOC = new TimeSpan(0, 0, 0);

	public static TimeSpan TimeToTOD = new TimeSpan(0, 0, 0);

	public static WayPoint customLocation = null;

	public static bool ProcessInfoFromSimulatorRunning = false;

	public static void ProcessSmallInfroFromSimulator(SimulatorConnectionManager.Struct2 simulatorData)
	{
		F.frmFlightPanelForm.ProcessSmallInfroFromSimulator(simulatorData);
		F.frmCompass.ProcessSmallInfroFromSimulator(simulatorData);
		F.frmHeliHelp.ProcessSmallInfroFromSimulator(simulatorData);
		if (F.tabControl1.SelectedTab == F.tabForceTrack)
		{
			F.plnForceTracker.Refresh();
		}
		if (F.tabControl1.SelectedTab == F.tabGraphicLog)
		{
			F.GraphicLogProcedure(simulatorData);
		}
		if (F.tabControl1.SelectedTab == F.tabStandardManouver)
		{
			F.labHeadingMag.Text = Utility.RadToDeg(simulatorData.PLANE_HEADING_RADIANT_MAGNETIC).ToString("F0");
			F.labHeadingTrue.Text = Utility.RadToDeg(simulatorData.PLANE_HEADING_RADIANT_TRUE).ToString("F0");
			double num = 10.0 * Utility.RadToDeg(simulatorData.PLANE_HEADING_RADIANT_MAGNETIC - lastSmallInfoSimulatorData.PLANE_HEADING_RADIANT_MAGNETIC);
			F.labDeltaHeading.Text = num.ToString("F1");
			F.labRefHeading.Text = simulatorData.autopilotheadinglockdir.ToString("F0");
			F.labRefHeading90.Text = Utility.NormalizeAngleDegree(simulatorData.autopilotheadinglockdir + 90.0).ToString("F0");
			F.labRefHeading180.Text = Utility.NormalizeAngleDegree(simulatorData.autopilotheadinglockdir + 180.0).ToString("F0");
			F.labRefHeading270.Text = Utility.NormalizeAngleDegree(simulatorData.autopilotheadinglockdir + 270.0).ToString("F0");
			F.PanelTurnSpeed.Tag = num;
			F.PanelTurnSpeed.Refresh();
			F.lblST_CurrentGS.Text = simulatorData.GROUND_VELOCITY.ToString("F0") + " kts";
			F.lblST_CurrentIas.Text = simulatorData.AIRSPEED_INDICATED.ToString("F0") + " kts";
			F.lblST_ExpectedBanking.Text = ((int)(simulatorData.AIRSPEED_INDICATED * 0.15)).ToString("F0") + "°";
			F.lblST_CurrentTurnRay.Text = (simulatorData.GROUND_VELOCITY / 60.0 / Math.PI).ToString("F1") + " nm";
		}
		lastSmallInfoSimulatorData = simulatorData;
		BuddyPilot.AiPilotProcedure(simulatorData, F, F.frmFlightPanelForm);
	}

	public static void ProcessInfoFromSimulator(SimulatorConnectionManager.Struct1 simulatorData)
	{
		if ((string)F.picBoxConnSpy.Tag == "OFF")
		{
			F.picBoxConnSpy.Tag = "ON";
			F.picBoxConnSpy.Image = F.imageList7X7.Images["Spy7x7on"];
		}
		else
		{
			F.picBoxConnSpy.Tag = "OFF";
			F.picBoxConnSpy.Image = F.imageList7X7.Images["Spy7x7off"];
		}
		if (simulatorData.atcid == "" || ProcessInfoFromSimulatorRunning)
		{
			return;
		}
		ProcessInfoFromSimulatorRunning = true;
		try
		{
			lastBigInfoSimulatorData = simulatorData;
			TimeSpan timeSpan = new TimeSpan(0, 0, (int)simulatorData.localtime);
			TimeSpan timeSpan2 = new TimeSpan(0, 0, (int)simulatorData.zulutime);
			SimLocal_Time = new DateTime(DateTime.Now.Year, 1, 1).AddDays(simulatorData.LOCAL_DAY_OF_YEAR) + timeSpan;
			SimZulu_Time = new DateTime(DateTime.Now.Year, 1, 1).AddDays(simulatorData.LOCAL_DAY_OF_YEAR) + timeSpan2;
			currentAircraft.SimLocal_Time = SimLocal_Time;
			currentAircraft.SimZulu_Time = SimZulu_Time;
			currentAircraft.position = new WayPoint(simulatorData.latitude, simulatorData.longitude, "CURRENT POSITION", "AIRPLANE", simulatorData.altitude);
			currentAircraft.title = simulatorData.title;
			if (F.btnFligthTrackRecord.Tag != null && (bool)F.btnFligthTrackRecord.Tag)
			{
				FligthTrackRecord.Add(currentAircraft.position.Clone());
			}
			F.labCurrentPOS.Text = simulatorData.latitude.ToString("F3") + "/" + simulatorData.longitude.ToString("F3");
			F.labCurrentAltitude.Text = simulatorData.altitude.ToString("F0") + " ft";
			F.labCurrentGS.Text = simulatorData.groundvelocity.ToString("F0") + " kn";
			F.labCurrentTime.Text = StandardFormatter.FormatDateTimeWithSeconds(SimLocal_Time) + "/" + StandardFormatter.FormatDateTimeWithSeconds(SimZulu_Time) + "Z";
			BuddyWorldManager.FlightCompactStatusNotification(F.labBuddyWorldFlightCompactStatus);
			currentAircraft.MAX_GROSS_WEIGHT = simulatorData.MAX_GROSS_WEIGHT;
			if (ZuluSimulationTime_10secAgo == DateTime.MinValue || (SimZulu_Time - ZuluSimulationTime_10secAgo).TotalSeconds >= 10.0 || (SimZulu_Time - ZuluSimulationTime_10secAgo).TotalSeconds < -1.0)
			{
				CheckFuel(simulatorData);
				currentAircraft.last10SecondsFuelflowGalPerSecond = (fuelQuantity_10secAgo - currentAircraft.fuelquantity) / (SimZulu_Time - ZuluSimulationTime_10secAgo).TotalSeconds;
				ZuluSimulationTime_10secAgo = SimZulu_Time;
				fuelQuantity_10secAgo = currentAircraft.fuelquantity;
			}
			if (FlightPlan.ATCWaypoints.Count > 0)
			{
				F.labArrivalTime.Text = StandardFormatter.FormatDateTime(FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].arrival_time_expected) + "Z";
				if (FlightPlan.ATCWaypoints[0].arrival_time_is_actual())
				{
					F.labElapsedTime.Text = StandardFormatter.FormatTimeSpan(SimZulu_Time - FlightPlan.ATCWaypoints[0].arrival_time_actual);
				}
				else
				{
					F.labElapsedTime.Text = "";
				}
				TimeSpan timeSpan3 = FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].arrival_time_expected - SimZulu_Time;
				if (timeSpan3.TotalSeconds > 0.0)
				{
					F.labRemainigTime.Text = StandardFormatter.FormatTimeSpan(timeSpan3);
					F.labArrivalTimeReal.Text = StandardFormatter.FormatDateTime(DateTime.Now + timeSpan3);
				}
				else
				{
					F.labRemainigTime.Text = "/";
					F.labArrivalTimeReal.Text = "/";
				}
			}
			if (FlightPlan.ATCWaypoints.Count == 0 || FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].arrival_time_is_actual())
			{
				nextWaypoint = null;
			}
			else
			{
				int i;
				for (i = 0; FlightPlan.ATCWaypoints[i].arrival_time_is_actual(); i++)
				{
				}
				nextWaypoint = FlightPlan.ATCWaypoints[i];
				nextWaypointIndex = i;
				double num = currentAircraft.position.DistanceFromMiles(nextWaypoint);
				if (FlightPlan.ATCWaypoints[0].arrival_time_is_actual())
				{
					elapsedMiles = nextWaypoint.Dist_sum_nm - num;
					F.labElapsedMiles.Text = elapsedMiles.ToString("F1") + "nm";
					F.labRemainingMiles.Text = (FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].Dist_sum_nm - elapsedMiles).ToString("F1") + "nm";
				}
				else
				{
					F.labElapsedMiles.Text = "0nm";
					F.labRemainingMiles.Text = FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].Dist_sum_nm.ToString("F1") + "nm";
					FlightPlan.DepartureTime = currentAircraft.SimZulu_Time;
				}
				if (nextWaypoint.preceeding == null)
				{
					F.labNextWaypointDescription.Text = "Take off from " + nextWaypoint.Id;
				}
				else if (nextWaypoint.following == null)
				{
					F.labNextWaypointDescription.Text = "Land at " + nextWaypoint.Id;
				}
				else
				{
					F.labNextWaypointDescription.Text = nextWaypoint.Id;
				}
				if (nextWaypoint.arrival_time_expected > SimZulu_Time)
				{
					if (i == 0)
					{
						F.labRemainigTimeWP.Text = StandardFormatter.FormatTimeSpanWithSeconds(nextWaypoint.arrival_time_expected - SimZulu_Time);
						F.labArrivalTimeWP.Text = StandardFormatter.FormatDateTime(nextWaypoint.arrival_time_expected);
						F.labRemainingMilesWP.Text = "";
					}
					else
					{
						if (nextWaypoint.avgTAS > 0.0)
						{
							nextWaypoint.calculated_arrival_time_expected_valud = true;
							if (Math.Abs(simulatorData.groundvelocity - nextWaypoint.avgTAS) < 50.0)
							{
								nextWaypoint.calculated_arrival_time_expected = SimZulu_Time + new TimeSpan(0, 0, (int)(3600.0 * num / simulatorData.groundvelocity));
							}
							else
							{
								nextWaypoint.calculated_arrival_time_expected = SimZulu_Time + new TimeSpan(0, 0, (int)(3600.0 * num / nextWaypoint.avgTAS));
							}
						}
						else
						{
							nextWaypoint.calculated_arrival_time_expected_valud = false;
						}
						F.labRemainigTimeWP.Text = StandardFormatter.FormatTimeSpanWithSeconds(nextWaypoint.arrival_time_expected - SimZulu_Time);
						F.labArrivalTimeWP.Text = StandardFormatter.FormatDateTime(nextWaypoint.arrival_time_expected) + "Z";
						F.labRemainingMilesWP.Text = num.ToString("F1") + " nm";
					}
				}
				if (F.radVnavStandard.Checked)
				{
					int num2 = FlightPlan.ATCWaypoints.Count - 3;
					if (nextWaypointIndex > num2)
					{
						num2 = nextWaypointIndex;
					}
					ATCWaypoint aTCWaypoint = FlightPlan.ATCWaypoints[num2];
					double num3 = (double)aTCWaypoint.Altitude - simulatorData.INDICATED_ALTITUDE;
					double num4 = new TimeSpan(0, 0, 0, (int)(60.0 * num3 / (double)F.nudDescFPM.Value), 0).TotalHours * FlightPlan.AverageTas(simulatorData.INDICATED_ALTITUDE, aTCWaypoint.Altitude, (double)F.nudDescIas.Value);
					double num5 = aTCWaypoint.Dist_sum_nm - elapsedMiles - num4;
					if (simulatorData.groundvelocity > 1.0)
					{
						TimeToTOD = new TimeSpan(0, 0, (int)(3600.0 * num5 / simulatorData.groundvelocity));
					}
					else
					{
						TimeToTOD = new TimeSpan(0, 0, 0);
					}
					if (TimeToTOD.Ticks < 0)
					{
						F.labVnavDescription.Text = "Descent";
						altitudeGoal = aTCWaypoint.Altitude;
						double num6 = FlightPlan.AverageTas(altitudeGoal, simulatorData.INDICATED_ALTITUDE, lastSmallInfoSimulatorData.AIRSPEED_INDICATED);
						double num7 = 3600.0 * (num4 / num6);
						fpmIdeal = num3 / num7 * 60.0;
					}
					else
					{
						double num8 = (double)FlightPlan.CruisingAlt - simulatorData.INDICATED_ALTITUDE;
						if (Math.Abs(num8) > 20.0)
						{
							if (num8 > 0.0)
							{
								fpmIdeal = Math.Min((double)F.nudClimbFPM.Value, 4.0 * num8);
								altitudeGoal = FlightPlan.CruisingAlt;
								deltafeet = num8;
								TimeToTOC = new TimeSpan(0, 0, 0, (int)(60.0 * num8 / fpmIdeal), 0);
								F.labVnavDescription.Text = "TOC in " + TimeToTOC;
							}
							else
							{
								fpmIdeal = Math.Max((double)F.nudDescFPM.Value, 4.0 * num8);
								altitudeGoal = FlightPlan.CruisingAlt;
								deltafeet = num8;
								TimeToTOC = new TimeSpan(0, 0, 0);
								F.labVnavDescription.Text = "Above TOC !";
							}
						}
						else
						{
							F.labVnavDescription.Text = "TOD in " + TimeToTOD;
							altitudeGoal = FlightPlan.CruisingAlt;
							fpmIdeal = num8;
							deltafeet = num8;
						}
					}
					F.labAltitudeGoal.Text = altitudeGoal.ToString("F0") + " ft";
					F.labFPMGoal.Text = fpmIdeal.ToString("F0") + " fpm";
					F.labSlopeGoal.Text = (57.2958 * Math.Atan2(deltafeet * 0.000164579, num)).ToString("F1") + "°";
					F.labVnavReference.Text = aTCWaypoint.Id;
				}
				else if (F.radVnavHoldAGL.Checked)
				{
					deltafeet = (double)F.nudVnavHoldAlt.Value - lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND;
					altitudeGoal = (int)((double)F.nudVnavHoldAlt.Value + (simulatorData.INDICATED_ALTITUDE - lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND));
					fpmIdeal = Utility.Clamp(deltafeet * 5.0, (double)F.nudDescFPM.Value, (double)F.nudClimbFPM.Value);
					F.labAltitudeGoal.Text = altitudeGoal.ToString("F0") + " ft";
					F.labFPMGoal.Text = fpmIdeal.ToString("F0") + " fpm";
					F.labSlopeGoal.Text = "--";
					F.labVnavReference.Text = "--";
				}
				if (i == 0)
				{
					if (simulatorData.altitude > (double)(nextWaypoint.Altitude + 100))
					{
						WayPointReached(SimZulu_Time);
					}
				}
				else if (num < 1.25 * AeronauticalUtilityFormulas.TurnRadiusMile(simulatorData.groundvelocity, (double)F.nudMaxBankAngle.Value))
				{
					WayPointReached(SimZulu_Time);
				}
			}
			if (F.tabControl1.SelectedTab == F.tabNavLog && F.dataGridView1.FirstDisplayedCell != null)
			{
				int rowIndex = F.dataGridView1.FirstDisplayedCell.RowIndex;
				int num9 = rowIndex + F.dataGridView1.DisplayedRowCount(includePartialRow: false) - 1;
				for (int j = 0; j < 5; j++)
				{
					lastInvalidatedRow++;
					if (lastInvalidatedRow > num9)
					{
						lastInvalidatedRow = rowIndex;
					}
					F.dataGridView1.InvalidateRow(lastInvalidatedRow);
				}
			}
			else if (F.tabControl1.SelectedTab == F.tabFuelManagement)
			{
				F.RefreshFuelManagementTab();
			}
			else if (F.tabControl1.SelectedTab == F.tabPathTrack)
			{
				F.MapPanel.Refresh();
			}
		}
		catch (Exception ex)
		{
			SimulatorConnectionManager.Sim_DisconnectFromSimulator();
			F.connectToolStripMenuItem.Checked = false;
			F.connectToolStripMenuItem.Text = "Connect simulator";
			currentAircraft = new Aircraft();
			MessageBox.Show(ex.Message);
		}
		ProcessInfoFromSimulatorRunning = false;
	}

	public static void WayPointReached(DateTime arrival_time_actual)
	{
		int i;
		for (i = 0; FlightPlan.ATCWaypoints[i].arrival_time_is_actual(); i++)
		{
		}
		FlightPlan.ATCWaypoints[i].arrival_time_actual = arrival_time_actual;
		F.dataGridView1.Refresh();
		if (i < FlightPlan.ATCWaypoints.Count - 1)
		{
			F.dataGridView1.CurrentCell = F.dataGridView1.Rows[i + 1].Cells[0];
		}
	}

	public static void ProcessAirplaneInfoFromSimulator(SimulatorConnectionManager.Struct10 airplaneData)
	{
		lastAirplaneinfoSimulatorData = airplaneData;
		if (F.tabControl1.SelectedTab == F.tabMisc)
		{
			F.displayAirplaneData(airplaneData);
		}
	}

	public static void CheckFuel(SimulatorConnectionManager.Struct1 simulatorData)
	{
		currentAircraft.FUEL_TANK_CENTER_QUANTITY = simulatorData.FUEL_TANK_CENTER_QUANTITY;
		currentAircraft.FUEL_TANK_CENTER2_QUANTITY = simulatorData.FUEL_TANK_CENTER2_QUANTITY;
		currentAircraft.FUEL_TANK_CENTER3_QUANTITY = simulatorData.FUEL_TANK_CENTER3_QUANTITY;
		currentAircraft.FUEL_TANK_LEFT_MAIN_QUANTITY = simulatorData.FUEL_TANK_LEFT_MAIN_QUANTITY;
		currentAircraft.FUEL_TANK_LEFT_AUX_QUANTITY = simulatorData.FUEL_TANK_LEFT_AUX_QUANTITY;
		currentAircraft.FUEL_TANK_LEFT_TIP_QUANTITY = simulatorData.FUEL_TANK_LEFT_TIP_QUANTITY;
		currentAircraft.FUEL_TANK_RIGHT_MAIN_QUANTITY = simulatorData.FUEL_TANK_RIGHT_MAIN_QUANTITY;
		currentAircraft.FUEL_TANK_RIGHT_AUX_QUANTITY = simulatorData.FUEL_TANK_RIGHT_AUX_QUANTITY;
		currentAircraft.FUEL_TANK_RIGHT_TIP_QUANTITY = simulatorData.FUEL_TANK_RIGHT_TIP_QUANTITY;
		currentAircraft.FUEL_TANK_EXTERNAL1_QUANTITY = simulatorData.FUEL_TANK_EXTERNAL1_QUANTITY;
		currentAircraft.FUEL_TANK_EXTERNAL2_QUANTITY = simulatorData.FUEL_TANK_EXTERNAL2_QUANTITY;
	}

	public static void SetApVerticalNavigationParameters(double Alt_feet, double VS_feetperminute)
	{
		Alt_feet = Math.Round(Alt_feet / 100.0) * 100.0;
		VS_feetperminute = Math.Round(VS_feetperminute / 100.0) * 100.0;
		SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT7, new SimulatorConnectionManager.Struct7
		{
			AUTOPILOT_ALTITUDE_LOCK_VAR = Alt_feet * 0.3048,
			AUTOPILOT_VERTICAL_HOLD_VAR = VS_feetperminute * 0.0050800001581309765
		});
	}

	public static void SetSurfaceControls(double aileron, double elevator, double rudder)
	{
		SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT8, new SimulatorConnectionManager.Struct8
		{
			AILERON_POSITION = aileron,
			ELEVATOR_POSITION = elevator,
			RUDDER_POSITION = rudder
		});
	}

	public static void SetDifferentialBrakes(double brakeLeftPercentage, double brakeRightPercentage)
	{
		uint dwData = (uint)(-16383.0 + 32766.0 * (brakeLeftPercentage / 100.0));
		uint dwData2 = (uint)(-16383.0 + 32766.0 * (brakeRightPercentage / 100.0));
		SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AXIS_LEFT_BRAKE_SET, dwData, SimulatorConnectionManager.GROUP.ID_PRIORITY_HIGHEST_MASKABLE, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
		SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AXIS_RIGHT_BRAKE_SET, dwData2, SimulatorConnectionManager.GROUP.ID_PRIORITY_HIGHEST_MASKABLE, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
	}

	public static void SetThrottle(double throttlePercentage)
	{
		SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT5, new SimulatorConnectionManager.Struct5
		{
			GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = throttlePercentage,
			GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = throttlePercentage
		});
	}
}
