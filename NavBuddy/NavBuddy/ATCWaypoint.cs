using System;

namespace NavBuddy;

public class ATCWaypoint : WayPoint
{
	public ATCWaypoint preceeding;

	public ATCWaypoint following;

	private double vIas;

	public DateTime calculated_arrival_time_expected;

	public bool calculated_arrival_time_expected_valud = false;

	public bool departure;

	public bool destination;

	public double IAS
	{
		get
		{
			if (preceeding == null)
			{
				return 0.0;
			}
			return vIas;
		}
		set
		{
			vIas = value;
		}
	}

	public double avgTAS
	{
		get
		{
			if (preceeding == null)
			{
				return 0.0;
			}
			return Math.Round(FlightPlan.AverageTas(preceeding.Altitude, base.Altitude, IAS), 0);
		}
	}

	public double Dist_nm
	{
		get
		{
			if (preceeding != null)
			{
				return DistanceFromMiles(preceeding);
			}
			return 0.0;
		}
	}

	public double Dist_sum_nm
	{
		get
		{
			if (preceeding != null)
			{
				return DistanceFromMiles(preceeding) + preceeding.Dist_sum_nm;
			}
			return 0.0;
		}
	}

	public int FPM
	{
		get
		{
			if (deltaAlt_feet() != 0.0)
			{
				return (int)(deltaAlt_feet() / time.TotalMinutes);
			}
			return 0;
		}
	}

	public double HDG_deg
	{
		get
		{
			if (preceeding != null)
			{
				return Math.Round(preceeding.CurrentBearingDegree(this), 0);
			}
			return 0.0;
		}
	}

	public TimeSpan time
	{
		get
		{
			if (preceeding != null)
			{
				return new TimeSpan(0, 0, (int)(3600.0 * Dist_nm / avgTAS));
			}
			return new TimeSpan(0L);
		}
	}

	public TimeSpan time_sum
	{
		get
		{
			if (preceeding != null)
			{
				return time + preceeding.time_sum;
			}
			return time;
		}
	}

	public DateTime arrival_time_expected
	{
		get
		{
			if (calculated_arrival_time_expected_valud)
			{
				return calculated_arrival_time_expected;
			}
			if (preceeding != null)
			{
				if (preceeding.arrival_time_is_actual())
				{
					return preceeding.arrival_time_actual.Add(time);
				}
				return preceeding.arrival_time_expected.Add(time);
			}
			return FlightPlan.DepartureTime;
		}
	}

	public DateTime arrival_time_actual { get; set; } = DateTime.MinValue;

	public double deltaAlt_feet()
	{
		if (preceeding != null)
		{
			return base.Altitude - preceeding.Altitude;
		}
		return 0.0;
	}

	public bool arrival_time_is_actual()
	{
		return arrival_time_actual > new DateTime(2000, 1, 1);
	}

	public ATCWaypoint(double latitudine, double longitudine, string identifier, string ATCWaypointType, double quoteFeet)
		: base(latitudine, longitudine, identifier, ATCWaypointType, quoteFeet)
	{
	}

	public ATCWaypoint(double latitudine, double longitudine, string identifier, string ATCWaypointType, string country)
		: base(latitudine, longitudine, identifier, ATCWaypointType, country)
	{
	}
}
