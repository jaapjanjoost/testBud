using System;
using System.Globalization;

namespace NavBuddy;

public class WayPoint
{
	public const double EARTH_RAY = 6373000.0;

	public string country;

	public string Id { get; set; }

	public string Type { get; set; }

	public double latitude { get; set; }

	public double longitude { get; set; }

	public int Altitude { get; set; }

	public string notes { get; set; }

	public string googleMapLink => ("https://www.google.com/maps/search/?api=1&query=" + latitude.ToString("G", CultureInfo.InvariantCulture) + "," + longitude.ToString("G", CultureInfo.InvariantCulture)) ?? "";

	public WayPoint Clone()
	{
		return new WayPoint(latitude, longitude, Id, Type, Altitude);
	}

	public double DistanceFromMiles(WayPoint secondary)
	{
		return DistanceFromMeters(secondary) * 0.000539957;
	}

	public WayPoint(double latitudine, double longitudine, string identifier, string ATCWaypointType, string country)
	{
		latitude = latitudine;
		longitude = longitudine;
		Id = identifier;
		Type = ATCWaypointType;
		this.country = country;
		Altitude = 0;
	}

	public WayPoint(double latitudine, double longitudine, string identifier, string ATCWaypointType, double quoteFeet)
	{
		latitude = latitudine;
		longitude = longitudine;
		Id = identifier;
		Type = ATCWaypointType;
		country = "";
		Altitude = (int)quoteFeet;
	}

	public WayPoint(string FseAirportDescription)
	{
		string[] array = FseAirportDescription.Split(new string[2] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
		try
		{
			Id = array[0];
			Type = "FSE Airport";
			Altitude = int.Parse(array[4].Split(',')[0].Replace("Elev:", "").Trim());
			string text = array[3].Split(',')[0].Replace("Lat: ", "");
			string text2 = array[3].Split(',')[1].Replace("Long: ", "");
			if (text.Contains("N"))
			{
				text = text.Replace("N", "");
				text = "+" + text.Trim();
			}
			else if (text.Contains("S"))
			{
				text = text.Replace("S", "");
				text = "-" + text.Trim();
			}
			if (text2.Contains("W"))
			{
				text2 = text2.Replace("W", "");
				text2 = "-" + text2.Trim();
			}
			else if (text2.Contains("E"))
			{
				text2 = text2.Replace("E", "");
				text2 = "+" + text2.Trim();
			}
			text = text.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
			text2 = text2.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
			latitude = double.Parse(text);
			longitude = double.Parse(text2);
		}
		catch (Exception)
		{
		}
	}

	public string GetSessagesimalCoordinates()
	{
		string text = "";
		string text2 = "";
		text2 = ((!(latitude < 0.0)) ? "N" : "S");
		text = ((!(longitude < 0.0)) ? "E" : "W");
		double num = Math.Abs(latitude);
		double num2 = Math.Abs(longitude);
		int num3 = (int)Math.Floor(num);
		int num4 = (int)Math.Floor((num - (double)num3) * 60.0);
		int num5 = (int)Math.Floor((num - (double)num3 - (double)num4 / 60.0) * 60.0 * 60.0);
		int num6 = (int)Math.Floor(num2);
		int num7 = (int)Math.Floor((num2 - (double)num6) * 60.0);
		int num8 = (int)Math.Floor((num2 - (double)num6 - (double)num7 / 60.0) * 60.0 * 60.0);
		return num3 + "° " + num4 + "' " + num5 + "\" " + text2 + num6 + "° " + num7 + "' " + num8 + "\" " + text;
	}

	protected double DistanceFromMeters(WayPoint secondary)
	{
		double d = Math.PI / 180.0 * latitude;
		double d2 = Math.PI / 180.0 * secondary.latitude;
		double num = Math.PI / 180.0 * (secondary.latitude - latitude);
		double num2 = Math.PI / 180.0 * (secondary.longitude - longitude);
		double num3 = Math.Sin(num / 2.0) * Math.Sin(num / 2.0) + Math.Cos(d) * Math.Cos(d2) * Math.Sin(num2 / 2.0) * Math.Sin(num2 / 2.0);
		double num4 = 2.0 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1.0 - num3));
		return 6373000.0 * num4;
	}

	public double CurrentBearingRad(WayPoint secondary)
	{
		double num = Math.PI / 180.0 * latitude;
		double num2 = Math.PI / 180.0 * secondary.latitude;
		double num3 = Math.PI / 180.0 * longitude;
		double num4 = Math.PI / 180.0 * secondary.longitude;
		double y = Math.Sin(num4 - num3) * Math.Cos(num2);
		double x = Math.Cos(num) * Math.Sin(num2) - Math.Sin(num) * Math.Cos(num2) * Math.Cos(num4 - num3);
		return Math.Atan2(y, x);
	}

	public double CurrentBearingDegree(WayPoint secondary)
	{
		double num;
		for (num = 180.0 / Math.PI * CurrentBearingRad(secondary); num < 0.0; num += 360.0)
		{
		}
		while (num > 360.0)
		{
			num -= 360.0;
		}
		return num;
	}

	public void Translate(double DegreeBearingNorth, double DistanceTravelledMeters)
	{
		double num = Math.PI / 180.0 * latitude;
		double num2 = Math.PI / 180.0 * longitude;
		double num3 = Math.PI / 180.0 * DegreeBearingNorth;
		double num4 = Math.Asin(Math.Sin(num) * Math.Cos(DistanceTravelledMeters / 6373000.0) + Math.Cos(num) * Math.Sin(DistanceTravelledMeters / 6373000.0) * Math.Cos(num3));
		double num5 = num2 + Math.Atan2(Math.Sin(num3) * Math.Sin(DistanceTravelledMeters / 6373000.0) * Math.Cos(num), Math.Cos(DistanceTravelledMeters / 6373000.0) - Math.Sin(num) * Math.Sin(num4));
		latitude = num4 * (180.0 / Math.PI);
		longitude = num5 * (180.0 / Math.PI);
	}

	public WayPoint()
	{
	}

	public override string ToString()
	{
		return "Lat:" + latitude.ToString("F3") + " Lng:" + longitude.ToString("F3") + " quote:" + Altitude.ToString("F0") + "ft";
	}

	public string googleEarthKmlString()
	{
		return longitude.ToString("G", CultureInfo.InvariantCulture) + "," + latitude.ToString("G", CultureInfo.InvariantCulture) + "," + (int)((double)Altitude / 3.28084);
	}
}
