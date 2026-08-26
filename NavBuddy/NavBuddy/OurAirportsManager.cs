using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace NavBuddy;

public static class OurAirportsManager
{
	private static List<OurAirportRunway> runways = new List<OurAirportRunway>();

	private static List<OurAirport> airports = new List<OurAirport>();

	private static void LoadAirports()
	{
		if (airports.Count == 0)
		{
			try
			{
				airports.Clear();
				AddAirportsFromFile("ourairports\\airports.csv");
				AddAirportsFromFile(DataManager.DataFolder() + "\\airports_custom.csv");
			}
			catch (Exception)
			{
				MessageBox.Show("Can't read OurAirports database. Please refer to https://ourairports.com/data/");
			}
		}
	}

	private static void LoadRunways()
	{
		if (runways.Count == 0)
		{
			try
			{
				runways.Clear();
				AddRunwaysFromFile("ourairports\\runways.csv");
				AddRunwaysFromFile(DataManager.DataFolder() + "\\runways_custom.csv");
			}
			catch (Exception)
			{
				MessageBox.Show("Can't read OurAirports runway database. Please refer to https://ourairports.com/data/");
			}
		}
	}

	private static void AddAirportsFromFile(string filename)
	{
		StreamReader streamReader = new StreamReader(filename);
		string text = streamReader.ReadLine();
		string[] array = text.Split(',');
		for (int i = 0; i < array.Length - 1; i++)
		{
			array[i] = Utility.RemoveQuotes(array[i]);
		}
		while (!streamReader.EndOfStream)
		{
			OurAirport ourAirport = new OurAirport();
			string text2 = streamReader.ReadLine();
			string[] array2 = text2.Split(',');
			for (int j = 0; j < array.Length - 1; j++)
			{
				PropertyInfo property = ourAirport.GetType().GetProperty(array[j]);
				TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
				if (typeCode == TypeCode.Object)
				{
					typeCode = Type.GetTypeCode(Nullable.GetUnderlyingType(property.PropertyType));
				}
				switch (typeCode)
				{
				case TypeCode.Double:
					property.SetValue(ourAirport, Utility.TryToDouble(array2[j]));
					break;
				case TypeCode.String:
					property.SetValue(ourAirport, Utility.RemoveQuotes(array2[j]));
					break;
				}
			}
			airports.Add(ourAirport);
		}
		streamReader.Close();
	}

	private static void AddRunwaysFromFile(string filename)
	{
		StreamReader streamReader = new StreamReader(filename);
		string text = streamReader.ReadLine();
		string[] array = text.Split(',');
		for (int i = 0; i < array.Length - 1; i++)
		{
			array[i] = Utility.RemoveQuotes(array[i]);
		}
		while (!streamReader.EndOfStream)
		{
			OurAirportRunway ourAirportRunway = new OurAirportRunway();
			string text2 = streamReader.ReadLine();
			string[] array2 = text2.Split(',');
			for (int j = 0; j < array.Length - 1; j++)
			{
				PropertyInfo property = ourAirportRunway.GetType().GetProperty(array[j]);
				TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
				if (typeCode == TypeCode.Object)
				{
					typeCode = Type.GetTypeCode(Nullable.GetUnderlyingType(property.PropertyType));
				}
				switch (typeCode)
				{
				case TypeCode.Double:
					property.SetValue(ourAirportRunway, Utility.TryToDouble(array2[j]));
					break;
				case TypeCode.String:
					property.SetValue(ourAirportRunway, Utility.RemoveQuotes(array2[j]));
					break;
				}
			}
			runways.Add(ourAirportRunway);
		}
		streamReader.Close();
	}

	public static void AddNewCustomRunway(OurAirportRunway runway)
	{
		runways.Add(runway);
		using StreamWriter streamWriter = File.AppendText(DataManager.DataFolder() + "\\runways_custom.csv");
		streamWriter.WriteLine(runway.GetSerializedString());
	}

	public static List<RunWay> GetRunways(string Icao)
	{
		LoadRunways();
		List<OurAirportRunway> list = runways.Where((OurAirportRunway R) => R.airport_ident == Icao).ToList();
		List<RunWay> list2 = new List<RunWay>();
		foreach (OurAirportRunway item in list)
		{
			if (item.HasValidRunways())
			{
				list2.Add(item.GetLeRunway());
				list2.Add(item.GetHeRunway());
			}
		}
		return list2;
	}

	public static OurAirport GetAirport(string IcaoIdent)
	{
		LoadAirports();
		return airports.Where((OurAirport A) => A.ident == IcaoIdent).FirstOrDefault();
	}

	public static OurAirport GetClosestAirport(WayPoint WP, out double mindistance, string exceptIdent = "UNDEFINED")
	{
		LoadAirports();
		OurAirport result = null;
		mindistance = double.MaxValue;
		foreach (OurAirport airport in airports)
		{
			if (airport.ident != exceptIdent)
			{
				double num = airport.GetWayPoint().DistanceFromMiles(WP);
				if (num < mindistance)
				{
					mindistance = num;
					result = airport;
				}
			}
		}
		return result;
	}

	public static List<OurAirport> GetAirportsWithinRange(OurAirport airport, double range)
	{
		LoadAirports();
		List<OurAirport> list = new List<OurAirport>();
		foreach (OurAirport airport2 in airports)
		{
			double num = airport2.GetWayPoint().DistanceFromMiles(airport.GetWayPoint());
			if (num < range && airport.ident != airport2.ident)
			{
				list.Add(airport2);
			}
		}
		return list;
	}

	public static List<OurAirport> GetAirportsWithinRange(WayPoint position, double range)
	{
		LoadAirports();
		List<OurAirport> list = new List<OurAirport>();
		foreach (OurAirport airport in airports)
		{
			double num = airport.GetWayPoint().DistanceFromMiles(position);
			if (num < range)
			{
				list.Add(airport);
			}
		}
		return list;
	}

	public static void prepareCustomFiles()
	{
		string path = DataManager.DataFolder() + "\\runways_custom.csv";
		if (!File.Exists(path))
		{
			File.Create(path).Dispose();
			using StreamWriter streamWriter = File.AppendText(path);
			streamWriter.WriteLine("\"id\",\"airport_ref\",\"airport_ident\",\"length_ft\",\"width_ft\",\"surface\",\"lighted\",\"closed\",\"le_ident\",\"le_latitude_deg\",\"le_longitude_deg\",\"le_elevation_ft\",\"le_heading_degT\",\"le_displaced_threshold_ft\",\"he_ident\",\"he_latitude_deg\",\"he_longitude_deg\",\"he_elevation_ft\",\"he_heading_degT\",\"he_displaced_threshold_ft\"");
		}
		string path2 = DataManager.DataFolder() + "\\airports_custom.csv";
		if (!File.Exists(path2))
		{
			File.Create(path2).Dispose();
			using StreamWriter streamWriter2 = File.AppendText(path2);
			streamWriter2.WriteLine("\"id\",\"ident\",\"type\",\"name\",\"latitude_deg\",\"longitude_deg\",\"elevation_ft\",\"continent\",\"iso_country\",\"iso_region\",\"municipality\",\"scheduled_service\",\"gps_code\",\"iata_code\",\"local_code\",\"home_link\",\"wikipedia_link\",\"keywords\"");
		}
	}

	public static List<string> GetAirportsTypes()
	{
		LoadAirports();
		return airports.Select((OurAirport a) => a.type).ToList().Distinct()
			.ToList();
	}
}
