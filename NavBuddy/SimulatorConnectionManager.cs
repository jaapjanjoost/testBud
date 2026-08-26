using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;
using NavBuddy;

public static class SimulatorConnectionManager
{
	public enum NOTIFICATION_GROUPS
	{
		GROUP0
	}

	public enum GROUP
	{
		ID_PRIORITY_STANDARD = 1900000000,
		ID_PRIORITY_HIGHEST_MASKABLE = 10000000
	}

	public enum DEFINITIONS1
	{
		STRUCT1,
		STRUCT2,
		STRUCT3,
		STRUCT4,
		STRUCT5,
		STRUCT6,
		STRUCT7,
		STRUCT8,
		STRUCT9,
		STRUCT10
	}

	public enum DATA_REQUESTS1
	{
		REQUEST1,
		REQUEST2,
		REQUEST3,
		REQUEST4
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct1
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string title;

		public double latitude;

		public double longitude;

		public double altitude;

		public double throttle;

		public double gear;

		public double localtime;

		public double zulutime;

		public double ZULU_DAY_OF_YEAR;

		public double LOCAL_DAY_OF_YEAR;

		public double groundvelocity;

		public double autopilotverticalholdvar;

		public double autopilotaltitudelockvar;

		public bool autopilotverticalhold;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string atcid;

		public double proprpm1;

		public double FUEL_TANK_CENTER_QUANTITY;

		public double FUEL_TANK_CENTER2_QUANTITY;

		public double FUEL_TANK_CENTER3_QUANTITY;

		public double FUEL_TANK_LEFT_MAIN_QUANTITY;

		public double FUEL_TANK_LEFT_AUX_QUANTITY;

		public double FUEL_TANK_LEFT_TIP_QUANTITY;

		public double FUEL_TANK_RIGHT_MAIN_QUANTITY;

		public double FUEL_TANK_RIGHT_AUX_QUANTITY;

		public double FUEL_TANK_RIGHT_TIP_QUANTITY;

		public double FUEL_TANK_EXTERNAL1_QUANTITY;

		public double FUEL_TANK_EXTERNAL2_QUANTITY;

		public double MAX_GROSS_WEIGHT;

		public double TOTAL_WEIGHT;

		public double EMPTY_WEIGHT;

		public double FUEL_TOTAL_QUANTITY_WEIGHT;

		public double INDICATED_ALTITUDE;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct2
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string title;

		public double autopilotverticalholdvar;

		public double autopilotaltitudelockvar;

		public bool autopilotverticalhold;

		public bool autopilotmaster;

		public bool autopilotaltitudelock;

		public bool autopilotheadinglock;

		public double autopilotheadinglockdir;

		public bool autopilotapproachhold;

		public bool autopilotnav1lock;

		public bool autopillotbackcoursehold;

		public bool autopilotflightdirectoractive;

		public double gforce;

		public double AIRSPEED_INDICATED;

		public double GROUND_VELOCITY;

		public double INDICATED_ALTITUDE;

		public double VERTICAL_SPEED;

		public double GENERAL_ENG_THROTTLE_LEVER_POSITION_1;

		public double PLANE_HEADING_RADIANT_TRUE;

		public double PLANE_HEADING_RADIANT_MAGNETIC;

		public double PLANE_ALT_ABOVE_GROUND;

		public bool BRAKE_PARKING_INDICATOR;

		public int FLAPS_HANDLE_PERCENT;

		public double latitude;

		public double longitude;

		public double altitude;

		public double MAGVAR;

		public double PLANE_BANK_RADIANT;

		public double PLANE_PITCH_RADIANT;

		public double INCIDENCE_ALPHA;

		public bool GEAR_HANDLE_POSITION;

		public double RUDDER_POSITION;

		public double AILERON_POSITION;

		public double ELEVATOR_POSITION;

		public double AMBIENT_WIND_DIRECTION;

		public double AMBIENT_WIND_VELOCITY;

		public double COLLECTIVE_POSITION;

		public double VELOCITY_BODY_X;

		public double VELOCITY_BODY_Z;

		public double ROTOR_LATERAL_TRIM_PCT;

		public double ROTOR_LONGITUDINAL_TRIM_PCT;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct3
	{
		public double latitude;

		public double longitude;

		public double altitude;

		public double plane_heading_degree_true;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct4
	{
		public double FUEL_TANK_CENTER_QUANTITY;

		public double FUEL_TANK_CENTER2_QUANTITY;

		public double FUEL_TANK_CENTER3_QUANTITY;

		public double FUEL_TANK_LEFT_MAIN_QUANTITY;

		public double FUEL_TANK_LEFT_AUX_QUANTITY;

		public double FUEL_TANK_LEFT_TIP_QUANTITY;

		public double FUEL_TANK_RIGHT_MAIN_QUANTITY;

		public double FUEL_TANK_RIGHT_AUX_QUANTITY;

		public double FUEL_TANK_RIGHT_TIP_QUANTITY;

		public double FUEL_TANK_EXTERNAL1_QUANTITY;

		public double FUEL_TANK_EXTERNAL2_QUANTITY;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct5
	{
		public double GENERAL_ENG_THROTTLE_LEVER_POSITION_1;

		public double GENERAL_ENG_THROTTLE_LEVER_POSITION_2;

		public double GENERAL_ENG_THROTTLE_LEVER_POSITION_3;

		public double GENERAL_ENG_THROTTLE_LEVER_POSITION_4;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct6
	{
		public double AUTOPILOT_HEADING_LOCK_DIR;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct7
	{
		public double AUTOPILOT_ALTITUDE_LOCK_VAR;

		public double AUTOPILOT_VERTICAL_HOLD_VAR;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct8
	{
		public double RUDDER_POSITION;

		public double AILERON_POSITION;

		public double ELEVATOR_POSITION;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct9
	{
		public double SPOILERS_HANDLE_POSITION;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Struct10
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string title;

		public double latitude;

		public double longitude;

		public double altitude;

		public double DESIGN_CRUISE_ALT;

		public double DESIGN_SPEED_VC;

		public double WING_AREA;

		public double WING_SPAN;

		public double FUEL_TOTAL_CAPACITY;

		public double FUEL_TOTAL_QUANTITY;

		public double FUEL_TOTAL_QUANTITY_WEIGHT;

		public double TOTAL_WEIGHT;

		public double EMPTY_WEIGHT;

		public double DESIGN_SPEED_VS0;
	}

	public enum EVENTS
	{
		FLAPS_INCR,
		FLAPS_DECR,
		GEAR_TOGGLE,
		AP_VS_VAR_INC,
		AP_VS_VAR_DEC,
		AP_ALT_VAR_INC,
		AP_ALT_VAR_DEC,
		AP_VS_HOLD,
		AP_ALT_HOLD,
		AP_MASTER,
		HEADING_BUG_INC,
		HEADING_BUG_DEC,
		AP_HDG_HOLD,
		AP_BC_HOLD,
		AP_NAV1_HOLD,
		AP_APR_HOLD,
		TOGGLE_FLIGHT_DIRECTOR,
		PARKING_BRAKES,
		AXIS_LEFT_BRAKE_SET,
		AXIS_RIGHT_BRAKE_SET,
		ENGINE_AUTO_SHUTDOWN,
		ENGINE_AUTO_START,
		MASTER_BATTERY_OFF,
		MASTER_BATTERY_ON
	}

	public static TextBox txtLog = null;

	public static FormMain myForm1 = null;

	public const int WM_USER_SIMCONNECT = 1026;

	public static SimConnect MySim;

	private static Dictionary<EVENTS, DateTime> eventsRegister = new Dictionary<EVENTS, DateTime>();

	public static void initSimData()
	{
		try
		{
			MySim.OnRecvSimobjectDataBytype += Sim_OnRecvSimobjectDataBytype;
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "GENERAL ENG THROTTLE LEVER POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "GEAR HANDLE POSITION", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "LOCAL TIME", "Seconds", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "ZULU TIME", "Seconds", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "ZULU DAY OF YEAR", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "LOCAL DAY OF YEAR", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "GROUND VELOCITY", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "AUTOPILOT VERTICAL HOLD VAR", "Feet/minute", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "AUTOPILOT ALTITUDE LOCK VAR", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "AUTOPILOT VERTICAL HOLD", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "ATC ID", null, SIMCONNECT_DATATYPE.STRING64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "PROP RPM:1", null, SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK CENTER QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK CENTER2 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK CENTER3 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK LEFT MAIN QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK LEFT AUX QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK LEFT TIP QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK RIGHT MAIN QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK RIGHT AUX QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK RIGHT TIP QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK EXTERNAL1 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TANK EXTERNAL2 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "MAX GROSS WEIGHT", "Pounds", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "TOTAL WEIGHT", "Pounds", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "EMPTY WEIGHT", "Pounds", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "FUEL TOTAL QUANTITY WEIGHT", "Pounds", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT1, "INDICATED ALTITUDE", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT VERTICAL HOLD VAR", "Feet/minute", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT ALTITUDE LOCK VAR", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT VERTICAL HOLD", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT MASTER", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT ALTITUDE LOCK", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT HEADING LOCK", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT HEADING LOCK DIR", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT APPROACH HOLD", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT NAV1 LOCK", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT BACKCOURSE HOLD", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AUTOPILOT FLIGHT DIRECTOR ACTIVE", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "G FORCE", "Gforce", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AIRSPEED INDICATED", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "GROUND VELOCITY", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "INDICATED ALTITUDE", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "VERTICAL SPEED", "Feet per second", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "PLANE HEADING DEGREES TRUE", "Radians", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "PLANE HEADING DEGREES MAGNETIC", "Radians", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "PLANE ALT ABOVE GROUND", "Feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "BRAKE PARKING INDICATOR", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "FLAPS HANDLE PERCENT", "Percent", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "MAGVAR", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "PLANE BANK DEGREES", "Radians", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "PLANE PITCH DEGREES", "Radians", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "INCIDENCE ALPHA", "Radians", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "GEAR HANDLE POSITION", "Bool", SIMCONNECT_DATATYPE.INT32, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "RUDDER POSITION", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AILERON POSITION", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "ELEVATOR POSITION", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AMBIENT WIND DIRECTION", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "AMBIENT WIND VELOCITY", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "COLLECTIVE POSITION", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "VELOCITY BODY X", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "VELOCITY BODY Z", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "ROTOR LATERAL TRIM PCT", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT2, "ROTOR LONGITUDINAL TRIM PCT", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT3, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT3, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT3, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT3, "PLANE HEADING DEGREES TRUE", "Radians", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK CENTER QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK CENTER2 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK CENTER3 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK LEFT MAIN QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK LEFT AUX QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK LEFT TIP QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK RIGHT MAIN QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK RIGHT AUX QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK RIGHT TIP QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK EXTERNAL1 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT4, "FUEL TANK EXTERNAL2 QUANTITY", "Gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT5, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT5, "GENERAL ENG THROTTLE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT5, "GENERAL ENG THROTTLE LEVER POSITION:3", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT5, "GENERAL ENG THROTTLE LEVER POSITION:4", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT6, "AUTOPILOT HEADING LOCK DIR", "Degree", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT7, "AUTOPILOT ALTITUDE LOCK VAR", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT7, "AUTOPILOT VERTICAL HOLD VAR", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT8, "RUDDER POSITION", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT8, "AILERON POSITION", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT8, "ELEVATOR POSITION", "", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT9, "SPOILERS HANDLE POSITION", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "DESIGN CRUISE ALT", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "DESIGN SPEED VC", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "WING AREA", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "WING SPAN", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "FUEL TOTAL CAPACITY", "gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "FUEL TOTAL QUANTITY", "gallons", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "FUEL TOTAL QUANTITY WEIGHT", "lbs", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "TOTAL WEIGHT", "lbs", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "EMPTY WEIGHT", "lbs", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.AddToDataDefinition(DEFINITIONS1.STRUCT10, "DESIGN SPEED VS0", "lbs", SIMCONNECT_DATATYPE.FLOAT64, 0f, SimConnect.SIMCONNECT_UNUSED);
			MySim.OnRecvEvent += Sim_OnRecvEvent;
			foreach (Enum value in Enum.GetValues(typeof(EVENTS)))
			{
				MySim.MapClientEventToSimEvent(value, ((EVENTS)(object)value/*cast due to constrained. prefix*/).ToString());
				MySim.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.GROUP0, value, bMaskable: false);
			}
			MySim.SetNotificationGroupPriority(NOTIFICATION_GROUPS.GROUP0, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);
			MySim.RegisterDataDefineStruct<Struct1>(DEFINITIONS1.STRUCT1);
			MySim.RegisterDataDefineStruct<Struct2>(DEFINITIONS1.STRUCT2);
			MySim.RegisterDataDefineStruct<Struct3>(DEFINITIONS1.STRUCT3);
			MySim.RegisterDataDefineStruct<Struct4>(DEFINITIONS1.STRUCT4);
			MySim.RegisterDataDefineStruct<Struct5>(DEFINITIONS1.STRUCT5);
			MySim.RegisterDataDefineStruct<Struct9>(DEFINITIONS1.STRUCT9);
			MySim.RegisterDataDefineStruct<Struct10>(DEFINITIONS1.STRUCT10);
			WriteLogNL("initSimData");
		}
		catch (COMException ex)
		{
			WriteLogNL("initSimData");
			WriteLogNL(ex.Message);
		}
	}

	public static void Sim_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
	{
		switch ((DATA_REQUESTS1)data.dwRequestID)
		{
		case DATA_REQUESTS1.REQUEST1:
		{
			Struct1 simulatorData2 = (Struct1)data.dwData[0];
			SimulatorInformationProcessing.ProcessInfoFromSimulator(simulatorData2);
			break;
		}
		case DATA_REQUESTS1.REQUEST2:
		{
			Struct2 simulatorData = (Struct2)data.dwData[0];
			SimulatorInformationProcessing.ProcessSmallInfroFromSimulator(simulatorData);
			break;
		}
		case DATA_REQUESTS1.REQUEST3:
		{
			Struct3 @struct = (Struct3)data.dwData[0];
			break;
		}
		case DATA_REQUESTS1.REQUEST4:
		{
			Struct10 airplaneData = (Struct10)data.dwData[0];
			SimulatorInformationProcessing.ProcessAirplaneInfoFromSimulator(airplaneData);
			break;
		}
		default:
			WriteLogNL("Unknown request ID: " + data.dwRequestID);
			break;
		}
	}

	public static void Sim_ConnectToSimulator(string Text, IntPtr Handle)
	{
		if (MySim != null)
		{
			return;
		}
		try
		{
			WriteLogNL("Try to connect...");
			MySim = new SimConnect(Text, Handle, 1026u, null, 0u);
			if (MySim != null)
			{
				MySim.OnRecvOpen += Sim_OnRecvOpen;
				MySim.OnRecvQuit += Sim_OnRecvQuit;
				initSimData();
			}
		}
		catch (COMException ex)
		{
			WriteLogNL("Unable to connect to MSFS:\n\n" + ex.Message);
		}
	}

	public static void Sim_DisconnectFromSimulator()
	{
		if (MySim != null)
		{
			MySim.Dispose();
			MySim = null;
			WriteLogNL("Disconnected");
		}
	}

	public static void Sim_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
	{
		WriteLogNL("Connected to MSFS");
	}

	public static void Sim_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
	{
		WriteLogNL("Disconnected from MSFS");
	}

	public static void Sim_RequestDataToSimConnect(DATA_REQUESTS1 Request, DEFINITIONS1 DataDefinition)
	{
		try
		{
			if (MySim != null)
			{
				MySim.RequestDataOnSimObjectType(Request, DataDefinition, 0u, SIMCONNECT_SIMOBJECT_TYPE.USER);
			}
			else
			{
				WriteLogNL("Not connected!");
			}
		}
		catch (COMException ex)
		{
			WriteLogNL(ex.Message);
		}
	}

	public static void Sim_TransmitDataToSimConnect(Enum StructDefinition, object DatatoBeTransmitted)
	{
		try
		{
			if (MySim != null)
			{
				MySim.SetDataOnSimObject(StructDefinition, 0u, SIMCONNECT_DATA_SET_FLAG.DEFAULT, DatatoBeTransmitted);
			}
			else
			{
				WriteLogNL("Not connected!");
			}
		}
		catch (COMException ex)
		{
			WriteLogNL(ex.Message);
		}
	}

	private static void Sim_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent)
	{
		try
		{
			EVENTS uEventID = (EVENTS)recEvent.uEventID;
			string text = uEventID.ToString();
			WriteLogNL("Sim_OnRecvEvent " + recEvent.uEventID + " " + recEvent.dwID + " " + recEvent.dwSize + " " + recEvent.dwVersion + " " + recEvent.uGroupID + text);
		}
		catch (Exception ex)
		{
			WriteLogNL("Exception Sim_OnRecvEvent " + ex.Message);
		}
	}

	public static void TransmitEvent_ControlledFrequency(EVENTS EventValue, int eventsToTransmit, float maxFrequency)
	{
		if (!eventsRegister.ContainsKey(EventValue) || (DateTime.Now - eventsRegister[EventValue]).TotalSeconds > (double)(1f / maxFrequency))
		{
			for (int i = 0; i < eventsToTransmit; i++)
			{
				MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EventValue, 0u, GROUP.ID_PRIORITY_HIGHEST_MASKABLE, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
			}
			object[] obj = new object[6]
			{
				eventsToTransmit.ToString(),
				" event ",
				EventValue,
				" ",
				null,
				null
			};
			EVENTS eVENTS = EventValue;
			obj[4] = eVENTS.ToString();
			obj[5] = " sent";
			WriteLog(string.Concat(obj));
			if (!eventsRegister.ContainsKey(EventValue))
			{
				eventsRegister.Add(EventValue, DateTime.Now);
			}
			else
			{
				eventsRegister[EventValue] = DateTime.Now;
			}
		}
	}

	public static void WriteLog(string text)
	{
		if (txtLog != null)
		{
			txtLog.AppendText(text);
		}
	}

	public static void WriteLogNL(string text)
	{
		WriteLog(text + Environment.NewLine);
	}

	public static void CleanLog()
	{
		if (txtLog != null)
		{
			txtLog.Clear();
		}
	}
}
