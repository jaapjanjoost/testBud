using System;
using System.Globalization;

namespace NavBuddy;

public class Aircraft : ISalvableDataObject
{
	public bool ActivationState = false;

	public DateTime activeSinceZulu;

	public DateTime SimLocal_Time;

	public DateTime SimZulu_Time;

	public double last10SecondsFuelflowGalPerSecond;

	public string title { get; set; }

	public string Atcid { get; set; }

	public WayPoint position { get; set; }

	public double flighthours { get; set; }

	public double FUEL_TANK_CENTER_QUANTITY { get; set; }

	public double FUEL_TANK_CENTER2_QUANTITY { get; set; }

	public double FUEL_TANK_CENTER3_QUANTITY { get; set; }

	public double FUEL_TANK_LEFT_MAIN_QUANTITY { get; set; }

	public double FUEL_TANK_LEFT_AUX_QUANTITY { get; set; }

	public double FUEL_TANK_LEFT_TIP_QUANTITY { get; set; }

	public double FUEL_TANK_RIGHT_MAIN_QUANTITY { get; set; }

	public double FUEL_TANK_RIGHT_AUX_QUANTITY { get; set; }

	public double FUEL_TANK_RIGHT_TIP_QUANTITY { get; set; }

	public double FUEL_TANK_EXTERNAL1_QUANTITY { get; set; }

	public double FUEL_TANK_EXTERNAL2_QUANTITY { get; set; }

	public double MAX_GROSS_WEIGHT { get; set; }

	public double fuelquantity
	{
		get
		{
			return FUEL_TANK_CENTER_QUANTITY + FUEL_TANK_CENTER2_QUANTITY + FUEL_TANK_CENTER3_QUANTITY + FUEL_TANK_LEFT_MAIN_QUANTITY + FUEL_TANK_LEFT_AUX_QUANTITY + FUEL_TANK_LEFT_TIP_QUANTITY + FUEL_TANK_RIGHT_MAIN_QUANTITY + FUEL_TANK_RIGHT_AUX_QUANTITY + FUEL_TANK_RIGHT_TIP_QUANTITY + FUEL_TANK_EXTERNAL1_QUANTITY + FUEL_TANK_EXTERNAL2_QUANTITY;
		}
		set
		{
		}
	}

	public string googleMapLink
	{
		get
		{
			if (position != null)
			{
				return ("https://www.google.com/maps/search/?api=1&query=" + position.latitude.ToString("G", CultureInfo.InvariantCulture) + "," + position.longitude.ToString("G", CultureInfo.InvariantCulture)) ?? "";
			}
			return "";
		}
	}

	string ISalvableDataObject.FileIdentifier()
	{
		return GetType().Name + "_" + Atcid;
	}

	public void DeleteFile()
	{
		DataManager.DeleteCorrespondingFile(this);
	}
}
