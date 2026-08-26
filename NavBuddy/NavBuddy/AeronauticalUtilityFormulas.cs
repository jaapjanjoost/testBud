using System;

namespace NavBuddy;

internal class AeronauticalUtilityFormulas
{
	public static double TurnRadiusMile(double groundSpeedKnots, double bankingDegree)
	{
		return Math.Pow(groundSpeedKnots, 2.0) / (11.294 * Math.Tan(Utility.DegToRad(bankingDegree))) / 6076.12;
	}
}
