namespace NavBuddy;

public class OurAirportRunway
{
	public string id { get; set; }

	public string airport_ref { get; set; }

	public string airport_ident { get; set; }

	public double? length_ft { get; set; }

	public double? width_ft { get; set; }

	public string surface { get; set; }

	public string lighted { get; set; }

	public string closed { get; set; }

	public string le_ident { get; set; }

	public double? le_latitude_deg { get; set; }

	public double? le_longitude_deg { get; set; }

	public double? le_elevation_ft { get; set; }

	public double? le_heading_degT { get; set; }

	public double? le_displaced_threshold_ft { get; set; }

	public string he_ident { get; set; }

	public double? he_latitude_deg { get; set; }

	public double? he_longitude_deg { get; set; }

	public double? he_elevation_ft { get; set; }

	public double? he_heading_degT { get; set; }

	public double? he_displaced_threshold_ft { get; set; }

	public bool HasValidRunways()
	{
		return le_latitude_deg.HasValue && le_longitude_deg.HasValue && le_elevation_ft.HasValue && he_latitude_deg.HasValue && he_longitude_deg.HasValue && he_elevation_ft.HasValue;
	}

	public RunWay GetLeRunway()
	{
		return new RunWay
		{
			runwayThreshold = new WayPoint(le_latitude_deg ?? 0.0, le_longitude_deg ?? 0.0, le_ident, "RUNWAY", le_elevation_ft ?? 0.0),
			runwayTerminal = new WayPoint(he_latitude_deg ?? 0.0, he_longitude_deg ?? 0.0, le_ident, "RUNWAY T", he_elevation_ft ?? 0.0),
			id = le_ident,
			airport_ref = airport_ref,
			airport_ident = airport_ident,
			length_ft = length_ft,
			width_ft = width_ft,
			surface = surface
		};
	}

	public string GetSerializedString()
	{
		string[] value = new string[20]
		{
			id,
			airport_ref,
			Utility.AddQuotes(airport_ident),
			Utility.FromDouble(length_ft),
			Utility.FromDouble(width_ft),
			Utility.AddQuotes(surface),
			lighted,
			closed,
			Utility.AddQuotes(le_ident),
			Utility.FromDouble(le_latitude_deg),
			Utility.FromDouble(le_longitude_deg),
			Utility.FromDouble(le_elevation_ft),
			Utility.FromDouble(le_heading_degT),
			Utility.FromDouble(le_displaced_threshold_ft),
			Utility.AddQuotes(he_ident),
			Utility.FromDouble(he_latitude_deg),
			Utility.FromDouble(he_longitude_deg),
			Utility.FromDouble(he_elevation_ft),
			Utility.FromDouble(he_heading_degT),
			Utility.FromDouble(he_displaced_threshold_ft)
		};
		return string.Join(",", value);
	}

	public RunWay GetHeRunway()
	{
		return new RunWay
		{
			runwayThreshold = new WayPoint(he_latitude_deg ?? 0.0, he_longitude_deg ?? 0.0, he_ident, "RUNWAY", he_elevation_ft ?? 0.0),
			runwayTerminal = new WayPoint(le_latitude_deg ?? 0.0, le_longitude_deg ?? 0.0, he_ident, "RUNWAY T", le_elevation_ft ?? 0.0),
			id = he_ident,
			airport_ref = airport_ref,
			airport_ident = airport_ident,
			length_ft = length_ft,
			width_ft = width_ft,
			surface = surface
		};
	}
}
