using System.Collections.Generic;
using System.Xml;

namespace NavBuddy.FSE;

public class FSEAirport
{
	public List<FSEAirport> connections = new List<FSEAirport>();

	public List<XmlNode> LoadedAssignment = new List<XmlNode>();

	public int assignmentFrom;

	public int assignmentTo;

	public string icao { get; set; }

	public double lat { get; set; }

	public double lon { get; set; }

	public string type { get; set; }

	public string size { get; set; }

	public string name { get; set; }

	public string city { get; set; }

	public string state { get; set; }

	public string country { get; set; }

	public WayPoint wayPoint { get; set; }

	public string fullname()
	{
		return icao + " " + name + " - " + city;
	}

	public int overallAssignments()
	{
		return assignmentFrom + assignmentTo;
	}
}
