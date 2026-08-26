using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NavBuddy.FSE;

public static class FseDataManager
{
	public static List<FSEAirport> fseAirports = new List<FSEAirport>();

	public static void ReloadFSEAirports(string icaodatapath)
	{
		try
		{
			StreamReader streamReader = new StreamReader(icaodatapath);
			fseAirports.Clear();
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				string[] array = text.Split(',');
				if (array[0] != "icao")
				{
					FSEAirport fSEAirport = new FSEAirport();
					fSEAirport.icao = array[0];
					fSEAirport.lat = Utility.toDouble(array[1]);
					fSEAirport.lon = Utility.toDouble(array[2]);
					fSEAirport.type = array[3];
					fSEAirport.size = array[4];
					fSEAirport.name = array[5];
					fSEAirport.city = array[6];
					fSEAirport.state = array[7];
					fSEAirport.country = array[8];
					fSEAirport.wayPoint = new ATCWaypoint(fSEAirport.lat, fSEAirport.lon, fSEAirport.icao, "", fSEAirport.country);
					fseAirports.Add(fSEAirport);
				}
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Can't read FS ICAO database. Please download icaodata.csv from http://server.fseconomy.net/datafeeds.jsp to enable FS-related functionalities.");
		}
	}
}
