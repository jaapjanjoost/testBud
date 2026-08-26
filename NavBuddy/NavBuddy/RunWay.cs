namespace NavBuddy;

public class RunWay
{
	private double vBearingDegreeTrue = double.MinValue;

	public string id { get; set; }

	public string airport_ref { get; set; }

	public string airport_ident { get; set; }

	public double? length_ft { get; set; }

	public double? width_ft { get; set; }

	public string surface { get; set; }

	public WayPoint runwayThreshold { get; set; }

	public WayPoint runwayTerminal { get; set; }

	public double BearingDegreeTrue()
	{
		if (vBearingDegreeTrue == double.MinValue)
		{
			vBearingDegreeTrue = runwayThreshold.CurrentBearingDegree(runwayTerminal);
		}
		return vBearingDegreeTrue;
	}

	public override string ToString()
	{
		return id + " - lenght:" + (length_ft ?? 0.0).ToString("F0") + "ft surface:" + surface + " alt:" + runwayThreshold.Altitude + "ft AMSL";
	}
}
