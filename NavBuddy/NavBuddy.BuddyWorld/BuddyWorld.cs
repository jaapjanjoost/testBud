using System;
using System.Collections.Generic;

namespace NavBuddy.BuddyWorld;

public class BuddyWorld : ISalvableDataObject
{
	public WayPoint yourPosition { get; set; } = new WayPoint(0.0, 0.0, "UNKOWN", "?", 0.0);

	public double money { get; set; } = 0.0;

	public double loan { get; set; } = 0.0;

	public double reputation { get; set; } = 10.0;

	public double OneShotInterestRatePercentage { get; set; } = 10.0;

	public int pilotWeight { get; set; } = 170;

	public string homeBase { get; set; } = null;

	public DateTime lastReputationProgress { get; set; } = DateTime.Now;

	public DateTime lastDamageChecking { get; set; } = DateTime.Now;

	public List<Airplane> airplanes { get; set; } = new List<Airplane>();

	public List<MoneyTransaction> transactions { get; set; } = new List<MoneyTransaction>();

	public List<Activity> activities { get; set; } = new List<Activity>();

	public string positionstackkey { get; set; }

	string ISalvableDataObject.FileIdentifier()
	{
		return "BuddyWorld";
	}

	public double maxloan()
	{
		return 72000.0 * Math.Log(1.0 + reputation / 10.0);
	}
}
