using System;

namespace NavBuddy.BuddyWorld;

public class MoneyTransaction
{
	public double value { get; set; }

	public DateTime date { get; set; }

	public string description { get; set; }

	public override string ToString()
	{
		return date.ToString() + "  " + value.ToString("F0").PadLeft(20, ' ') + " $  " + description;
	}
}
