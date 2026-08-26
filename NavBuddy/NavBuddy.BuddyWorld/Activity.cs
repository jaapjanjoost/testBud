using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NavBuddy.PassengerGenerator;
using NavBuddy.Positionstack;

namespace NavBuddy.BuddyWorld;

public class Activity
{
	public string id { get; set; }

	public string ReferenceAirportIdent { get; set; }

	public ActivityType activityType { get; set; }

	public string Description { get; set; }

	public string ExtendedDescription { get; set; }

	public List<PayLoad> payloads { get; set; } = new List<PayLoad>();

	public double Reward { get; set; }

	public bool Accepted { get; set; } = false;

	public bool Completed { get; set; } = false;

	public DateTime ExpiringDate { get; set; }

	public double DurationHours { get; set; }

	public bool Failed { get; set; } = false;

	public string ActivityAirplaneRegistration { get; set; }

	public DateTime ActivityDueDateTime { get; set; } = DateTime.MinValue;

	public Activity()
	{
	}

	public bool Expired()
	{
		return ExpiringDate < DateTime.Now.Date;
	}

	public Activity(string id, string ReferenceAirportIdent, ActivityType activityType)
	{
		this.id = id;
		this.ReferenceAirportIdent = ReferenceAirportIdent;
		this.activityType = activityType;
		ExpiringDate = DateTime.Now.Date + new TimeSpan(1, 0, 0, 0);
	}

	public static Activity GenerateActivity(WayPoint generateActivityLocation)
	{
		int seed = (DateTime.Now.Year * 1000 + DateTime.Now.DayOfYear + DateTime.Now.Millisecond) % int.MaxValue;
		Random random = new Random(seed);
		OurAirport sourceAirport = null;
		OurAirport ourAirport = null;
		Activity activity = null;
		int index = random.Next(Enum.GetValues(typeof(ActivityType)).Length);
		ActivityType activityType = (ActivityType)Enum.GetValues(typeof(ActivityType)).GetValue(index);
		string text = Enum.GetName(typeof(ActivityType), activityType) + random.Next(100000);
		switch (activityType)
		{
		case ActivityType.AirTaxi:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 10.0)
				where new string[4] { "small_airport", "seaplane_base", "medium_airport", "large_airport" }.Contains(a.type)
				select a).ToList(), random);
			ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 60.0)
				where new string[4] { "small_airport", "seaplane_base", "medium_airport", "large_airport" }.Contains(a.type) && a.ident != sourceAirport.ident
				select a).ToList(), random);
			if (sourceAirport != null && ourAirport != null)
			{
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Deliver single passenger to destination";
				activity.payloads = new List<PayLoad> { GenerateRandomPassenger(random, sourceAirport.GetWayPoint(), ourAirport.GetWayPoint()) };
				activity.Reward = Utility.CubizeValue(10, (2.5 + random.NextDouble()) * 2.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint()));
			}
			break;
		case ActivityType.FlyDay:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 15.0)
				where new string[2] { "small_airport", "seaplane_base" }.Contains(a.type)
				select a).ToList(), random);
			if (sourceAirport != null)
			{
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Entertain passenger with a small fly around the airport";
				activity.payloads = new List<PayLoad>
				{
					GenerateRandomPassenger(random, sourceAirport.GetWayPoint(), sourceAirport.GetWayPoint()),
					GenerateRandomPassenger(random, sourceAirport.GetWayPoint(), sourceAirport.GetWayPoint()),
					GenerateRandomPassenger(random, sourceAirport.GetWayPoint(), sourceAirport.GetWayPoint())
				};
				activity.Reward = 10.0;
			}
			break;
		case ActivityType.FamilyOnHoliday:
		{
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 10.0)
				where new string[4] { "small_airport", "seaplane_base", "medium_airport", "large_airport" }.Contains(a.type)
				select a).ToList(), random);
			ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 150.0)
				where new string[4] { "small_airport", "seaplane_base", "medium_airport", "large_airport" }.Contains(a.type) && a.ident != sourceAirport.ident
				select a).ToList(), random);
			string familySurname = "";
			List<PayLoad> list = GenerateRandomFamily(random, sourceAirport.GetWayPoint(), ourAirport.GetWayPoint(), out familySurname, WithBaggage: true);
			if (sourceAirport != null && ourAirport != null)
			{
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Deliver " + familySurname + " family to their holiday destination";
				activity.payloads = list;
				activity.Reward = Utility.CubizeValue(50, (2.5 + random.NextDouble()) * 2.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint()));
			}
			break;
		}
		case ActivityType.MiniCargoExpress:
		{
			double degreeBearingNorth = 360.0 * random.NextDouble();
			double distanceTravelledMeters = 3000.0 + 2000.0 * random.NextDouble();
			WayPoint wayPoint = generateActivityLocation.Clone();
			wayPoint.Translate(degreeBearingNorth, distanceTravelledMeters);
			wayPoint.Id = "unknown";
			PositionStackData positionStackData = PositionStackManager.ReverseLocation(wayPoint);
			if (positionStackData != null)
			{
				wayPoint = positionStackData.PossibleValidPosition();
			}
			double degreeBearingNorth2 = 360.0 * random.NextDouble();
			double distanceTravelledMeters2 = 10000.0 + 30000.0 * random.NextDouble();
			WayPoint wayPoint2 = generateActivityLocation.Clone();
			wayPoint2.Translate(degreeBearingNorth2, distanceTravelledMeters2);
			wayPoint2.Id = "unknown";
			PositionStackData positionStackData2 = PositionStackManager.ReverseLocation(wayPoint2);
			if (positionStackData2 != null)
			{
				wayPoint2 = positionStackData2.PossibleValidPosition();
			}
			if (wayPoint2 != null && wayPoint != null)
			{
				activity = new Activity(text, "UNKOWN", activityType);
				activity.Description = "Deliver cargo";
				activity.payloads = new List<PayLoad> { GenerateRandomCargo(random, wayPoint, wayPoint2) };
				activity.Reward = 250.0;
			}
			break;
		}
		case ActivityType.AirShuttle:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 40.0)
				where new string[2] { "medium_airport", "large_airport" }.Contains(a.type)
				select a).ToList(), random);
			ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 15.0)
				where new string[2] { "small_airport", "seaplane_base" }.Contains(a.type) && a.ident != sourceAirport.ident
				select a).ToList(), random);
			if (sourceAirport != null && ourAirport != null)
			{
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = $"Connect {sourceAirport.name} with {ourAirport} for a local event. Passengers will spawn continuously";
				activity.payloads = new List<PayLoad>
				{
					GenerateRandomPassenger(random, sourceAirport.GetWayPoint(), ourAirport.GetWayPoint()),
					GenerateRandomPassenger(random, ourAirport.GetWayPoint(), sourceAirport.GetWayPoint())
				};
				activity.Reward = Utility.CubizeValue(1, (0.5 + random.NextDouble() * 0.2) * 2.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint()));
			}
			break;
		case ActivityType.Cargo:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 10.0)
				where new string[2] { "medium_airport", "large_airport" }.Contains(a.type)
				select a).ToList(), random);
			if (sourceAirport != null)
			{
				ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 200.0)
					where new string[2] { "medium_airport", "large_airport" }.Contains(a.type) && a.ident != sourceAirport.ident
					select a).ToList(), random);
			}
			if (sourceAirport != null && ourAirport != null)
			{
				double num6 = 200.0 + 5000.0 * Math.Pow(random.NextDouble(), 2.0);
				double num7;
				for (num7 = 25.0; num6 / num7 > 10.0; num7 += 25.0)
				{
				}
				int num8 = (int)Math.Round(num6 / num7);
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Deliver Cargo to destination";
				activity.payloads = new List<PayLoad>();
				for (int num9 = 1; num9 <= num8; num9++)
				{
					activity.payloads.Add(new PayLoad
					{
						Type = "CARGO",
						Description = "Cargo Module " + num9,
						WeightLb = num7,
						Position = sourceAirport.GetWayPoint(),
						Destination = ourAirport.GetWayPoint()
					});
				}
				activity.Reward = Utility.CubizeValue(10, (0.9 + 0.2 * random.NextDouble()) * (num6 / 100.0) * 2.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint()));
				activity.ExpiringDate = DateTime.Now + new TimeSpan(7, 0, 0, 0);
			}
			break;
		case ActivityType.CompanyTransfer:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 35.0)
				where new string[2] { "medium_airport", "large_airport" }.Contains(a.type)
				select a).ToList(), random);
			if (sourceAirport != null)
			{
				ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 1000.0)
					where new string[2] { "medium_airport", "large_airport" }.Contains(a.type) && a.ident != sourceAirport.ident
					select a).ToList(), random);
			}
			if (sourceAirport != null && ourAirport != null)
			{
				int num2 = (int)(2.0 + 13.0 * Math.Pow(random.NextDouble(), 2.0));
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Deliver complany member to destination";
				activity.payloads = new List<PayLoad>();
				string companyName = PassengerManager.CompanyName(random);
				for (int num3 = 1; num3 <= num2; num3++)
				{
					activity.payloads.Add(GenerateRandomCompanyPassenger(random, sourceAirport.GetWayPoint(), ourAirport.GetWayPoint(), companyName));
				}
				activity.Reward = Utility.CubizeValue(10, (0.9 + 0.2 * random.NextDouble()) * (double)num2 * 2.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint()));
				activity.ExpiringDate = DateTime.Now + new TimeSpan(1, 0, 0, 0);
			}
			break;
		case ActivityType.AirplaneRental:
		{
			Airplane[] array = BuddyWorldManager.world.airplanes.Where((Airplane A) => A.bodyDamage == 0.0 && A.engineDamage == 0.0 && A.nextAvailableMoment <= DateTime.Now).ToArray();
			if (array.Length != 0)
			{
				Airplane airplane = array[random.Next(0, array.Length)];
				if (airplane != null)
				{
					double num10 = random.NextDouble() + 8.0 * random.NextDouble() + 24.0 * random.NextDouble();
					activity = new Activity(text, airplane.position.Id, activityType);
					activity.Description = "Request for rental for " + airplane.registration + ". Duration: " + num10.ToString("F0") + " hours";
					activity.Reward = Math.Round((0.9 + random.NextDouble() * 0.2) * airplane.rentalPricePerHour() * num10);
					activity.ExpiringDate = DateTime.Now + new TimeSpan(1, 0, 0, 0);
					activity.ActivityAirplaneRegistration = airplane.registration;
					activity.DurationHours = num10;
				}
			}
			break;
		}
		case ActivityType.Getaway:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 15.0)
				where new string[3] { "small_airport", "medium_airport", "large_airport" }.Contains(a.type)
				select a).ToList(), random);
			if (sourceAirport != null)
			{
				ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 100.0)
					where new string[2] { "medium_airport", "large_airport" }.Contains(a.type) && a.ident != sourceAirport.ident && a.iso_country != sourceAirport.iso_country
					select a).ToList(), random);
			}
			if (sourceAirport != null && ourAirport != null)
			{
				TimeSpan timeSpan = new TimeSpan(0, 0, (int)(36.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint())));
				int num4 = (int)(1.0 + Math.Floor(3.0 * random.NextDouble()));
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Take this people beyond the country border as soon as possible. Ensure to fly during night.";
				activity.Description += "\r\nLoad people on board after 10 PM and unload them before 5AM";
				Activity activity2 = activity;
				activity2.Description = activity2.Description + "\r\nDeliver everyone within " + timeSpan.ToString() + " since first passenger is onboard";
				activity.payloads = new List<PayLoad>();
				for (int num5 = 1; num5 <= num4; num5++)
				{
					activity.payloads.Add(GenerateRandomPassenger(random, sourceAirport.GetWayPoint(), ourAirport.GetWayPoint(), " unknown"));
				}
				activity.Reward = 1000 + Utility.CubizeValue(1000, random.NextDouble() * 4000.0);
				activity.ExpiringDate = DateTime.Now + new TimeSpan(1, 0, 0, 0);
			}
			break;
		case ActivityType.IllicitCargo:
			sourceAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(generateActivityLocation, 10.0)
				where new string[2] { "small_airport", "medium_airport" }.Contains(a.type)
				select a).ToList(), random);
			if (sourceAirport != null)
			{
				ourAirport = Utility.RandomListSelector((from a in OurAirportsManager.GetAirportsWithinRange(sourceAirport, 120.0)
					where new string[2] { "small_airport", "medium_airport" }.Contains(a.type) && a.ident != sourceAirport.ident && a.iso_country != sourceAirport.iso_country
					select a).ToList(), random);
			}
			if (sourceAirport != null && ourAirport != null)
			{
				double num = 50.0 + random.NextDouble() * 250.0;
				activity = new Activity(text, sourceAirport.ident, activityType);
				activity.Description = "Deliver illicit cargo to destination. Stay below 1000 feet AGL or you'll fail.";
				activity.payloads = new List<PayLoad>();
				activity.payloads.Add(new PayLoad
				{
					Type = "CARGO",
					Description = "Illicit cargo",
					WeightLb = num,
					Position = sourceAirport.GetWayPoint(),
					Destination = ourAirport.GetWayPoint()
				});
				activity.Reward = Utility.CubizeValue(10, 20.0 * (0.9 + 0.2 * random.NextDouble()) * (num / 100.0) * 2.0 * sourceAirport.GetWayPoint().DistanceFromMiles(ourAirport.GetWayPoint()));
				activity.ExpiringDate = DateTime.Now + new TimeSpan(5, 0, 0, 0);
			}
			break;
		default:
			throw new NotImplementedException();
		}
		return activity;
	}

	public static List<PayLoad> GenerateRandomFamily(Random RND, WayPoint Position, WayPoint Destination, out string familySurname, bool WithBaggage = false)
	{
		List<PayLoad> list = new List<PayLoad>();
		OurAirport ourAirport = ((!(RND.NextDouble() > 0.5)) ? OurAirportsManager.GetAirport(Destination.Id) : OurAirportsManager.GetAirport(Position.Id));
		string country = "";
		if (ourAirport != null)
		{
			country = ourAirport.iso_country;
		}
		int num = 25 + RND.Next(30);
		Passenger passenger = PassengerManager.GenerateRandomPassenger(RND, country, num, "m");
		PayLoad item = new PayLoad
		{
			Type = "PASSENGER",
			Description = passenger.Description(),
			WeightLb = passenger.WeightPounds,
			Position = Position,
			Destination = Destination
		};
		list.Add(item);
		familySurname = passenger.Surname;
		int num2 = Utility.Clamp(num - RND.Next(10), 23, 100);
		Passenger passenger2 = PassengerManager.GenerateRandomPassenger(RND, country, num2, "f");
		PayLoad item2 = new PayLoad
		{
			Type = "PASSENGER",
			Description = passenger2.Description(),
			WeightLb = passenger2.WeightPounds,
			Position = Position,
			Destination = Destination
		};
		list.Add(item2);
		int num3 = num2 - (18 + RND.Next(22));
		while (num3 > 0 && RND.NextDouble() < 0.66)
		{
			Passenger passenger3 = PassengerManager.GenerateRandomPassenger(RND, country, num3);
			passenger3.Surname = familySurname;
			PayLoad item3 = new PayLoad
			{
				Type = "PASSENGER",
				Description = passenger3.Description(),
				WeightLb = passenger3.WeightPounds,
				Position = Position,
				Destination = Destination
			};
			num3 -= 1 + RND.Next(8);
			list.Add(item3);
		}
		PayLoad item4 = new PayLoad
		{
			Type = "CARGO",
			Description = "family " + familySurname + " baggage",
			WeightLb = (int)(1.0 + RND.NextDouble() * 30.0 * (double)list.Count),
			Position = Position,
			Destination = Destination
		};
		list.Add(item4);
		return list;
	}

	public static PayLoad GenerateRandomCargo(Random RND, WayPoint Position, WayPoint Destination)
	{
		return new PayLoad
		{
			Type = "CARGO",
			Description = "CARGO",
			WeightLb = Utility.CubizeValue(10, 10.0 + 200.0 * RND.NextDouble()),
			Position = Position,
			Destination = Destination
		};
	}

	public static PayLoad GenerateRandomPassenger(Random RND, WayPoint Position, WayPoint Destination, string specificprofession = "")
	{
		OurAirport ourAirport = ((!(RND.NextDouble() > 0.5)) ? OurAirportsManager.GetAirport(Destination.Id) : OurAirportsManager.GetAirport(Position.Id));
		string country = "";
		if (ourAirport != null)
		{
			country = ourAirport.iso_country;
		}
		Passenger passenger = PassengerManager.GenerateRandomPassenger(RND, country);
		if (specificprofession != "")
		{
			passenger.Profession = specificprofession;
		}
		return new PayLoad
		{
			Type = "PASSENGER",
			Description = passenger.Description(),
			WeightLb = passenger.WeightPounds,
			Position = Position,
			Destination = Destination
		};
	}

	public static PayLoad GenerateRandomCompanyPassenger(Random RND, WayPoint Position, WayPoint Destination, string CompanyName)
	{
		OurAirport airport = OurAirportsManager.GetAirport(Position.Id);
		string country = "";
		if (airport != null)
		{
			country = airport.iso_country;
		}
		Passenger passenger = PassengerManager.GenerateRandomPassenger(RND, country);
		passenger.Profession = CompanyName + " employee";
		return new PayLoad
		{
			Type = "PASSENGER",
			Description = passenger.Description(),
			WeightLb = passenger.WeightPounds,
			Position = Position,
			Destination = Destination
		};
	}

	public void AcceptedActivityRun(double timerPeriodSeconds)
	{
		Random random = new Random();
		switch (activityType)
		{
		case ActivityType.AirplaneRental:
			if (!Completed)
			{
				Airplane airplane = BuddyWorldManager.world.airplanes.Where((Airplane AP) => AP.registration == ActivityAirplaneRegistration).FirstOrDefault();
				if (airplane != null)
				{
					airplane.nextAvailableMoment = DateTime.Now + new TimeSpan(0, 0, (int)(DurationHours * 3600.0));
					double num2 = DurationHours * (0.25 + random.NextDouble() * 0.75);
					airplane.BodyConsumption(num2 * 3600.0);
					airplane.EngineConsumption(num2 * 3600.0);
					BuddyWorldManager.AttemptGain(Math.Round(Reward), "Payment for " + Description);
					Completed = true;
					BuddyWorldManager.RaiseEventNotifyRefreshActivities();
					BuddyWorldManager.SaveBuddyWorld();
				}
			}
			break;
		case ActivityType.AirShuttle:
			if (payloads.Where((PayLoad P) => !P.Delivered && !P.Loaded && P.Destination.Id == payloads[1].Destination.Id).Count() < 1)
			{
				payloads.Add(GenerateRandomPassenger(random, payloads[0].Destination.Clone(), payloads[1].Destination.Clone()));
				BuddyWorldManager.RaiseEventNotifyRefreshActivities();
			}
			if (payloads.Where((PayLoad P) => !P.Delivered && !P.Loaded && P.Destination.Id == payloads[0].Destination.Id).Count() < 1)
			{
				payloads.Add(GenerateRandomPassenger(random, payloads[1].Destination.Clone(), payloads[0].Destination.Clone()));
				BuddyWorldManager.RaiseEventNotifyRefreshActivities();
			}
			break;
		case ActivityType.FlyDay:
		{
			foreach (PayLoad item in BuddyWorldManager.CurrentLoadedPayload())
			{
				if (SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND > 1000.0)
				{
					item.tmpReached1000FeetAGL = true;
				}
			}
			int num = payloads.Where((PayLoad P) => !P.Delivered).Count();
			if (random.NextDouble() < 0.03 / (1.0 + (double)num))
			{
				payloads.Add(GenerateRandomPassenger(random, payloads[0].Destination.Clone(), payloads[0].Destination.Clone()));
			}
			break;
		}
		case ActivityType.IllicitCargo:
			if (SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND > 1000.0)
			{
				Failed = true;
			}
			break;
		case ActivityType.Getaway:
		{
			if (payloads.Where((PayLoad P) => P.Loaded).Count() <= 0)
			{
				break;
			}
			if (ActivityDueDateTime == DateTime.MinValue)
			{
				PayLoad payLoad = payloads.Where((PayLoad P) => P.Loaded).FirstOrDefault();
				ActivityDueDateTime = DateTime.Now + new TimeSpan(0, 0, (int)(36.0 * payLoad.Position.DistanceFromMiles(payLoad.Destination)));
			}
			if (DateTime.Now > ActivityDueDateTime)
			{
				Failed = true;
			}
			TimeSpan timeSpan = new TimeSpan(0, 0, (int)SimulatorInformationProcessing.lastBigInfoSimulatorData.localtime);
			if (timeSpan > new TimeSpan(5, 0, 0) && timeSpan < new TimeSpan(22, 0, 0))
			{
				Failed = true;
			}
			break;
		}
		case ActivityType.FamilyOnHoliday:
		case ActivityType.MiniCargoExpress:
		case ActivityType.Cargo:
		case ActivityType.CompanyTransfer:
			break;
		}
	}

	public void PayloadDelivered(PayLoad deliveredPayload)
	{
		switch (activityType)
		{
		case ActivityType.FlyDay:
			if (deliveredPayload.tmpReached1000FeetAGL)
			{
				BuddyWorldManager.AttemptGain(Reward, "Payment for tourist flight: " + deliveredPayload.Description);
			}
			BuddyWorldManager.world.reputation += 0.5;
			return;
		case ActivityType.AirShuttle:
			BuddyWorldManager.world.reputation += 0.5;
			BuddyWorldManager.AttemptGain(Reward, "Payment for AirShuttle transfer: " + deliveredPayload.Description);
			return;
		case ActivityType.Getaway:
		{
			bool flag = true;
			foreach (PayLoad payload in payloads)
			{
				flag = flag && payload.Delivered;
			}
			if (flag)
			{
				Completed = true;
				BuddyWorldManager.world.reputation -= 4.0;
				if (Failed)
				{
					MessageBox.Show("Getaway failed!");
				}
				else
				{
					BuddyWorldManager.AttemptGain(Reward, "Payment for " + Description);
				}
			}
			return;
		}
		case ActivityType.IllicitCargo:
		{
			bool flag2 = true;
			foreach (PayLoad payload2 in payloads)
			{
				flag2 = flag2 && payload2.Delivered;
			}
			if (flag2)
			{
				Completed = true;
				BuddyWorldManager.world.reputation -= 4.0;
				if (Failed)
				{
					MessageBox.Show("Illicit cargo failed!");
				}
				else
				{
					BuddyWorldManager.AttemptGain(Reward, "Payment for " + Description);
				}
			}
			return;
		}
		}
		bool flag3 = true;
		foreach (PayLoad payload3 in payloads)
		{
			flag3 = flag3 && payload3.Delivered;
		}
		if (flag3)
		{
			BuddyWorldManager.AttemptGain(Reward, "Payment for " + Description);
			Completed = true;
		}
		if (deliveredPayload.Type == "PASSENGER")
		{
			BuddyWorldManager.world.reputation++;
		}
		else if ((deliveredPayload.Type == "CARGO") & flag3)
		{
			BuddyWorldManager.world.reputation++;
		}
	}
}
