using System;

namespace NavBuddy.BuddyWorld;

public class Airplane
{
	public const double REFERENCE_WINGSPAN_FT = 30.0;

	public const double REFERENCE_DESIGN_CRUISE_ALT_FT = 5000.0;

	public const double REFERENCE_DESIGN_CRUISE_SPEED_KMPH = 200.0;

	public const double BODY_STANDARD_CONSUMPTION_PER_HOUR = 0.002;

	public const double ENGINE_STANDARD_CONSUMPTION_PER_HOUR = 0.002;

	public string registration { get; set; }

	public string title { get; set; }

	public WayPoint position { get; set; }

	public double heading { get; set; }

	public double flightHours { get; set; } = 0.0;

	public int completeFlights { get; set; } = 0;

	public double flightMileage { get; set; } = 0.0;

	public double bodyStatus { get; set; } = 1.0;

	public double engineStatus { get; set; } = 1.0;

	public double bodyDamage { get; set; } = 0.0;

	public double engineDamage { get; set; } = 0.0;

	public DateTime nextAvailableMoment { get; set; } = DateTime.Now;

	public bool rented { get; set; } = false;

	public DateTime rentedSince { get; set; } = DateTime.Now;

	public double rentedLat { get; set; }

	public double rentedLon { get; set; }

	public double wingspan_ft { get; set; }

	public double design_cruise_alt_ft { get; set; }

	public double design_cruise_speed_kmph { get; set; }

	public double fuel_total_capacity { get; set; }

	public bool helicopter { get; set; }

	public double quotation { get; set; } = 0.0;

	public double fuelgal { get; set; } = 0.0;

	public Airplane(SimulatorConnectionManager.Struct10 airplanedata)
	{
		position = new WayPoint(airplanedata.latitude, airplanedata.longitude, "Airplane position", "", airplanedata.altitude);
		double mindistance = 0.0;
		OurAirport closestAirport = OurAirportsManager.GetClosestAirport(position, out mindistance);
		if (mindistance < 2.0)
		{
			position = closestAirport.GetWayPoint();
		}
		helicopter = airplanedata.DESIGN_SPEED_VS0 < 10.0;
		title = airplanedata.title;
		wingspan_ft = airplanedata.WING_SPAN;
		design_cruise_alt_ft = airplanedata.DESIGN_CRUISE_ALT;
		design_cruise_speed_kmph = airplanedata.DESIGN_SPEED_VC;
		fuel_total_capacity = airplanedata.FUEL_TOTAL_CAPACITY;
		if (wingspan_ft == 0.0)
		{
			wingspan_ft = 30.0;
		}
		if (design_cruise_alt_ft == 0.0)
		{
			design_cruise_alt_ft = 5000.0;
		}
		if (design_cruise_speed_kmph == 0.0)
		{
			design_cruise_speed_kmph = 200.0;
		}
	}

	public Airplane()
	{
	}

	public double CurrentMarketValue()
	{
		return AbsoluteMarketValue() - CompleteBodyMaintenanceCost() - CompleteEngineMaintenanceCost();
	}

	public double AbsoluteMarketValue()
	{
		return 18000.0 * Math.Pow(wingspan_ft / 30.0, 2.0) * Math.Pow(design_cruise_alt_ft / 5000.0, 0.25) * Math.Pow(design_cruise_speed_kmph / 200.0, 1.7) * (double)((!helicopter) ? 1 : 6);
	}

	public void BodyConsumption(double appliedFlightTimeSeconds)
	{
		double num = Math.Round(0.002 * (appliedFlightTimeSeconds / 3600.0), 5);
		bodyStatus -= num;
	}

	public void EngineConsumption(double appliedFlightTimeSeconds)
	{
		double num = Math.Round(0.002 * (appliedFlightTimeSeconds / 3600.0), 5);
		engineStatus -= num;
	}

	public double BodyMaintenanceCost(double bodyMaintenanceFraction)
	{
		return AbsoluteMarketValue() * bodyMaintenanceFraction * 0.25;
	}

	public double BodyRepairCost(double bodyDamageFraction)
	{
		return AbsoluteMarketValue() * bodyDamageFraction * 0.5;
	}

	public double CompleteBodyMaintenanceCost()
	{
		return BodyMaintenanceCost(1.0 - bodyStatus);
	}

	public double EngineMaintenanceCost(double engineMaintenanceFraction)
	{
		return AbsoluteMarketValue() * engineMaintenanceFraction * 0.25;
	}

	public double EngineRepairCost(double EngineDamageFraction)
	{
		return AbsoluteMarketValue() * EngineDamageFraction * 0.5;
	}

	public double CompleteEngineMaintenanceCost()
	{
		return EngineMaintenanceCost(1.0 - engineStatus);
	}

	public override string ToString()
	{
		return registration + " - " + title;
	}

	public double rentalPricePerHour()
	{
		return 2.0 * (EngineMaintenanceCost(0.002) + BodyMaintenanceCost(0.002));
	}
}
