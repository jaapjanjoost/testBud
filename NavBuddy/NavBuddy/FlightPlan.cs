using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NavBuddy;

public static class FlightPlan
{
	public static List<ATCWaypoint> ATCWaypoints = new List<ATCWaypoint>();

	public static ATCWaypoint Departure;

	public static ATCWaypoint Destination;

	public static RunWay TakeOffRunway;

	public static RunWay LandingRunway;

	public static string Title { get; set; }

	public static string FPType { get; set; }

	public static int CruisingAlt { get; set; }

	public static string DepartureID { get; set; }

	public static string DepartureLLA { get; set; }

	public static string DestinationID { get; set; }

	public static string DestinationLLA { get; set; }

	public static string Descr { get; set; }

	public static string DepartureName { get; set; }

	public static string DestinationName { get; set; }

	public static DateTime DepartureTime { get; set; }

	public static void LoadPlanFromPLNFile(string filename)
	{
		XmlDocument xmlDocument = new XmlDocument();
		FileStream inStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
		xmlDocument.Load(inStream);
		LoadPlanFromXlmNode(xmlDocument.GetElementsByTagName("FlightPlan.FlightPlan")[0]);
	}

	private static void LoadPlanFromXlmNode(XmlNode XmlSource)
	{
		ATCWaypoints.Clear();
		PropertyInfo[] properties = typeof(FlightPlan).GetProperties();
		foreach (XmlNode childNode in XmlSource.ChildNodes)
		{
			try
			{
				if (childNode.Name == "ATCWaypoint")
				{
					string innerText = childNode.SelectSingleNode("WorldPosition").InnerText;
					string[] array = innerText.Split(',');
					double latitudine = ConvertCoord(array[0]);
					double longitudine = ConvertCoord(array[1]);
					string value = childNode.Attributes["id"].Value;
					string innerText2 = childNode.SelectSingleNode("ATCWaypointType").InnerText;
					double quoteFeet = double.Parse(array[2], CultureInfo.InvariantCulture.NumberFormat);
					ATCWaypoints.Add(new ATCWaypoint(latitudine, longitudine, value, innerText2, quoteFeet));
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						if (childNode2.Name == "RunwayNumberFP")
						{
							ATCWaypoint aTCWaypoint = ATCWaypoints.Last();
							aTCWaypoint.notes = aTCWaypoint.notes + "RWY: " + childNode2.InnerText + " ";
						}
						if (childNode2.Name == "DepartureFP")
						{
							ATCWaypoint aTCWaypoint2 = ATCWaypoints.Last();
							aTCWaypoint2.notes = aTCWaypoint2.notes + "Departure: " + childNode2.InnerText + " ";
						}
						if (childNode2.Name == "ArrivalFP")
						{
							ATCWaypoint aTCWaypoint3 = ATCWaypoints.Last();
							aTCWaypoint3.notes = aTCWaypoint3.notes + "Arrival: " + childNode2.InnerText + " ";
						}
					}
				}
				if (childNode.Name == "CruisingAlt")
				{
					CruisingAlt = (int)double.Parse(childNode.InnerText, CultureInfo.InvariantCulture.NumberFormat);
				}
				else
				{
					PropertyInfo property = typeof(FlightPlan).GetProperty(childNode.Name);
					if (property != null)
					{
						property.SetValue(null, childNode.InnerText);
					}
				}
				if (childNode.Name == "DepartureLLA")
				{
					string[] array2 = childNode.InnerText.Split(',');
					double latitudine2 = ConvertCoord(array2[0]);
					double longitudine2 = ConvertCoord(array2[1]);
					double quoteFeet2 = double.Parse(array2[2], CultureInfo.InvariantCulture.NumberFormat);
					Departure = new ATCWaypoint(latitudine2, longitudine2, "Departure", "Airport", quoteFeet2);
				}
				if (childNode.Name == "DestinationLLA")
				{
					string[] array3 = childNode.InnerText.Split(',');
					double latitudine3 = ConvertCoord(array3[0]);
					double longitudine3 = ConvertCoord(array3[1]);
					double quoteFeet3 = double.Parse(array3[2], CultureInfo.InvariantCulture.NumberFormat);
					Destination = new ATCWaypoint(latitudine3, longitudine3, "Destination", "Airport", quoteFeet3);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		for (int i = 0; i < ATCWaypoints.Count - 1; i++)
		{
			ATCWaypoints[i].following = ATCWaypoints[i + 1];
			ATCWaypoints[i + 1].preceeding = ATCWaypoints[i];
		}
	}

	public static void DefaultAltitudeAssignments()
	{
		for (int i = 0; i < ATCWaypoints.Count; i++)
		{
			if (i > 0 && i < ATCWaypoints.Count - 1 && ATCWaypoints[i].Altitude == 0)
			{
				ATCWaypoints[i].Altitude = CruisingAlt;
			}
		}
	}

	public static void CalculateTocAndTod(double ClimbSpeed, double DescSpeed, double ClimbRate, double DescRate, ATCWaypoint riferimentoTOD = null)
	{
		double num = CruisingAlt - ATCWaypoints[0].Altitude;
		double num2 = CruisingAlt - ATCWaypoints[ATCWaypoints.Count - 1].Altitude;
		double num3 = 60.0 * (num / ClimbRate);
		double num4 = 60.0 * (num2 / (0.0 - DescRate));
		double num5 = num3 * AverageTas(CruisingAlt, ATCWaypoints[0].Altitude, ClimbSpeed) / 3600.0;
		double num6 = num4 * AverageTas(CruisingAlt, ATCWaypoints[ATCWaypoints.Count - 1].Altitude, DescSpeed) / 3600.0;
		int i;
		for (i = 0; ATCWaypoints[i].Dist_sum_nm < num5; i++)
		{
		}
		ATCWaypoint aTCWaypoint = InsertWaypoint(i, (num5 - ATCWaypoints[i - 1].Dist_sum_nm) / ATCWaypoints[i].Dist_nm, "planned TOC", CruisingAlt);
		if (i > 1)
		{
			for (int j = 1; j < i; j++)
			{
				ATCWaypoints[j].Altitude = ATCWaypoints[0].Altitude + (int)(num * (ATCWaypoints[j].Dist_sum_nm / num5));
			}
		}
		if (riferimentoTOD != null)
		{
			int num7 = ATCWaypoints.FindIndex((ATCWaypoint WP) => WP == riferimentoTOD);
			int altitude = riferimentoTOD.Altitude;
			num2 = CruisingAlt - altitude;
			num4 = 60.0 * (num2 / (0.0 - DescRate));
			num6 = num4 * AverageTas(CruisingAlt, riferimentoTOD.Altitude, DescSpeed) / 3600.0;
			double num8 = riferimentoTOD.Dist_sum_nm - num6;
			i = num7 + 1;
			while (ATCWaypoints[i].Dist_sum_nm > num8)
			{
				i--;
				if (i < 0)
				{
					MessageBox.Show("Impossible calculate TOD");
					return;
				}
			}
			ATCWaypoint aTCWaypoint2 = InsertWaypoint(i + 1, (num8 - ATCWaypoints[i].Dist_sum_nm) / ATCWaypoints[i + 1].Dist_nm, "planned TOD", CruisingAlt);
			for (int num9 = num7; num9 > i + 1; num9--)
			{
				ATCWaypoints[num9].Altitude = altitude + (int)(num2 * (1.0 - (ATCWaypoints[num9].Dist_sum_nm - num8) / num6));
			}
		}
		else
		{
			i = ATCWaypoints.Count - 1;
			double num10 = ATCWaypoints[ATCWaypoints.Count - 1].Dist_sum_nm - num6;
			while (ATCWaypoints[i].Dist_sum_nm > num10)
			{
				i--;
			}
			ATCWaypoint aTCWaypoint3 = InsertWaypoint(i + 1, (num10 - ATCWaypoints[i].Dist_sum_nm) / ATCWaypoints[i + 1].Dist_nm, "planned TOD", CruisingAlt);
			for (int num11 = ATCWaypoints.Count - 2; num11 > i + 1; num11--)
			{
				ATCWaypoints[num11].Altitude = ATCWaypoints[ATCWaypoints.Count - 1].Altitude + (int)(num2 * (1.0 - (ATCWaypoints[num11].Dist_sum_nm - num10) / num6));
			}
		}
	}

	public static ATCWaypoint InsertWaypoint(int followingWaypointIndex, double proximityToFollowingFactor, string identifier, double altitude)
	{
		double num = 1.0 - proximityToFollowingFactor;
		double latitudine = ATCWaypoints[followingWaypointIndex - 1].latitude * num + ATCWaypoints[followingWaypointIndex].latitude * proximityToFollowingFactor;
		double longitudine = ATCWaypoints[followingWaypointIndex - 1].longitude * num + ATCWaypoints[followingWaypointIndex].longitude * proximityToFollowingFactor;
		ATCWaypoint aTCWaypoint = new ATCWaypoint(latitudine, longitudine, identifier, "calculated", altitude);
		aTCWaypoint.preceeding = ATCWaypoints[followingWaypointIndex - 1];
		aTCWaypoint.following = ATCWaypoints[followingWaypointIndex];
		ATCWaypoints[followingWaypointIndex - 1].following = aTCWaypoint;
		ATCWaypoints[followingWaypointIndex].preceeding = aTCWaypoint;
		ATCWaypoints.Insert(followingWaypointIndex, aTCWaypoint);
		return aTCWaypoint;
	}

	public static double AverageTas(double altitudeFrom, double altitudeTo, double IAS)
	{
		return 0.5 * (IAS * Math.Pow(1.02, altitudeFrom / 1000.0) + IAS * Math.Pow(1.02, altitudeTo / 1000.0));
	}

	public static void DefaultSpeedAssignments(double ClimbSpeed, double CruiseSpeed, double DescSpeed)
	{
		for (int i = 0; i < ATCWaypoints.Count; i++)
		{
			if (i == 0)
			{
				ATCWaypoints[i].IAS = 0.0;
			}
			else if (ATCWaypoints[i].deltaAlt_feet() > 50.0)
			{
				ATCWaypoints[i].IAS = ClimbSpeed;
			}
			else if (ATCWaypoints[i].deltaAlt_feet() < -50.0)
			{
				ATCWaypoints[i].IAS = DescSpeed;
			}
			else
			{
				ATCWaypoints[i].IAS = CruiseSpeed;
			}
		}
	}

	private static double ConvertCoord(string coordAsString)
	{
		string[] array = coordAsString.Split(' ');
		double num = 0.0;
		double num2 = 0.0;
		if (array[0].Substring(0, 1) == "N")
		{
			num = int.Parse(array[0].Replace("N", "").Replace("°", ""));
			num2 = 1.0;
		}
		if (array[0].Substring(0, 1) == "E")
		{
			num = int.Parse(array[0].Replace("E", "").Replace("°", ""));
			num2 = 1.0;
		}
		if (array[0].Substring(0, 1) == "W")
		{
			num = int.Parse(array[0].Replace("W", "").Replace("°", ""));
			num2 = -1.0;
		}
		if (array[0].Substring(0, 1) == "S")
		{
			num = int.Parse(array[0].Replace("S", "").Replace("°", ""));
			num2 = -1.0;
		}
		double num3 = 1.0 / 60.0 * double.Parse(array[1].Replace("'", ""), CultureInfo.InvariantCulture.NumberFormat);
		double num4 = 0.0002777777777777778 * double.Parse(array[2].Replace("\"", ""), CultureInfo.InvariantCulture.NumberFormat);
		return num2 * (num + num3 + num4);
	}

	public static void ExportToGoogleEarth(string filename)
	{
		string text = GoogleEarthExporter.flightPlanTemplate();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (ATCWaypoint aTCWaypoint in ATCWaypoints)
		{
			stringBuilder.Append(aTCWaypoint.googleEarthKmlString());
			stringBuilder.Append(" ");
		}
		string text2 = text.Replace("@COORDINATES@", stringBuilder.ToString().Trim());
		text2 = text2.Replace("@DOCUMENTNAME@", Title);
		text2 = text2.Replace("@NAME@", Title);
		File.WriteAllText(filename, text2);
		SimulatorConnectionManager.WriteLogNL("Saved " + filename);
	}
}
