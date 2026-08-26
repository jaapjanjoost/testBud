namespace NavBuddy;

public class OurAirport
{
	public string id { get; set; }

	public string ident { get; set; }

	public string type { get; set; }

	public string name { get; set; }

	public double? latitude_deg { get; set; }

	public double? longitude_deg { get; set; }

	public double? elevation_ft { get; set; }

	public string continent { get; set; }

	public string iso_country { get; set; }

	public string iso_region { get; set; }

	public string municipality { get; set; }

	public string scheduled_service { get; set; }

	public string gps_code { get; set; }

	public string iata_code { get; set; }

	public string local_code { get; set; }

	public string home_link { get; set; }

	public string wikipedia_link { get; set; }

	public string keywords { get; set; }

	public WayPoint GetWayPoint()
	{
		return new WayPoint(latitude_deg ?? 0.0, longitude_deg ?? 0.0, ident, "AIRPORT", elevation_ft ?? 0.0);
	}

	public string DetailedDescription()
	{
		return $"\r\n                      ICAO/IDENT: {ident}  NAME: {name}\r\n                      lat:{latitude_deg} lon:{longitude_deg} elevation: {elevation_ft}\r\n                      {type} - {continent} - {iso_country} - {iso_region} - {municipality}\r\n                      service: {scheduled_service} \r\n                      GPS_CODE: {gps_code} IATA: {iata_code} LOCAL: {local_code}\r\n                      home: {home_link} wiki: {wikipedia_link}\r\n                      keywords: {keywords}                       \r\n                    ";
	}
}
