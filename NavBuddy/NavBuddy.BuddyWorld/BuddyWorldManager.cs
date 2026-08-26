using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NavBuddy.BuddyWorld;

public static class BuddyWorldManager
{
	private enum FlightPhase
	{
		NotActive,
		WaitingForTakeOff,
		OnGoing
	}

	public static BuddyWorld world = null;

	public static Airplane quotatedAirplane = null;

	public static Airplane selectedPlane = null;

	public static Airplane flyingPlane = null;

	public static bool WorldRunning = false;

	public static string flightProblemDescription = "";

	public const double FUEL_PRICE = 1.0;

	public static double flightStartZuluTime;

	public static WayPoint flightStartLocation;

	public static double flightEngineConsumptionTimeSeconds;

	public static double flightBodyConsumptionTimeSeconds;

	public const double OPERATIONAL_RANGE_NM = 2.0;

	public const double PASSENGER_PRICE_PER_NM = 2.0;

	public const double CARGO_PRICE_PER_100LBS_PER_NM = 2.0;

	private static FlightPhase flightPhase = FlightPhase.NotActive;

	public static event EventHandler NotifyRefresh = delegate
	{
	};

	public static event EventHandler NotifyRefreshActivities = delegate
	{
	};

	public static void WorldRun(double timerPeriodSeconds)
	{
		if (WorldRunning)
		{
			return;
		}
		WorldRunning = true;
		try
		{
			if (world == null)
			{
				world = (BuddyWorld)DataManager.LoadObjectFromFile("BuddyWorld", typeof(BuddyWorld));
				if (world == null)
				{
					world = new BuddyWorld();
				}
				CleanupLoadedPayload();
				CleanupExpiredActivities();
			}
			if (BuddyWorldManager.flightPhase == FlightPhase.WaitingForTakeOff && SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND > 50.0)
			{
				BuddyWorldManager.flightPhase = FlightPhase.OnGoing;
			}
			if (BuddyWorldManager.flightPhase == FlightPhase.OnGoing && SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND < 50.0 && SimulatorInformationProcessing.lastSmallInfoSimulatorData.GROUND_VELOCITY < 0.5 && SimulatorInformationProcessing.lastSmallInfoSimulatorData.BRAKE_PARKING_INDICATOR)
			{
				EndFlight();
			}
			FlightPhase flightPhase = BuddyWorldManager.flightPhase;
			if (flightPhase != FlightPhase.NotActive && (uint)(flightPhase - 1) <= 1u)
			{
				double gENERAL_ENG_THROTTLE_LEVER_POSITION_ = SimulatorInformationProcessing.lastSmallInfoSimulatorData.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
				if (SimulatorInformationProcessing.lastSmallInfoSimulatorData.GROUND_VELOCITY > 1.0)
				{
					flightBodyConsumptionTimeSeconds += timerPeriodSeconds;
					if (gENERAL_ENG_THROTTLE_LEVER_POSITION_ < 90.0)
					{
						flightEngineConsumptionTimeSeconds += timerPeriodSeconds;
					}
					else
					{
						flightEngineConsumptionTimeSeconds += timerPeriodSeconds * (gENERAL_ENG_THROTTLE_LEVER_POSITION_ - 89.0);
					}
				}
			}
			if (DateTime.Now - world.lastReputationProgress > new TimeSpan(8, 0, 0))
			{
				if (world.reputation < 100.0)
				{
					world.reputation += 10.0;
				}
				world.lastReputationProgress += new TimeSpan(8, 0, 0);
				SaveBuddyWorld();
			}
			foreach (Activity item in world.activities.Where((Activity A) => A.Accepted))
			{
				item.AcceptedActivityRun(timerPeriodSeconds);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		WorldRunning = false;
	}

	public static void AskLoan(double NewLoanDemandedValue)
	{
		double num = world.loan + NewLoanDemandedValue * (1.0 + world.OneShotInterestRatePercentage * 0.01);
		if (num < world.maxloan())
		{
			AttemptGain(NewLoanDemandedValue, "Loan requested");
			world.loan = num;
			SaveBuddyWorld();
		}
		else
		{
			MessageBox.Show("The requested loan is too much compared to your asset value");
		}
	}

	public static void RepayLoan(double RepayLoanValue)
	{
		if (RepayLoanValue > world.loan)
		{
			RepayLoanValue = world.loan;
		}
		if (AttemptExpense(RepayLoanValue, "Loan payment"))
		{
			world.loan -= RepayLoanValue;
			SaveBuddyWorld();
		}
		else
		{
			MessageBox.Show("You have not that money");
		}
	}

	public static string FlightDescription()
	{
		double num = SimulatorInformationProcessing.lastBigInfoSimulatorData.zulutime;
		if (num < flightStartZuluTime)
		{
			num += 86000.0;
		}
		string text = (num - flightStartZuluTime).ToString("F0") + "s";
		switch (flightPhase)
		{
		case FlightPhase.NotActive:
			if (flightProblemDescription != "")
			{
				return flightProblemDescription;
			}
			return "Not active";
		case FlightPhase.WaitingForTakeOff:
			return "Waiting for take off..." + text;
		case FlightPhase.OnGoing:
			return "On Going..." + text;
		default:
			return "Unknown";
		}
	}

	public static void FlightCompactStatusNotification(Label L)
	{
		switch (flightPhase)
		{
		case FlightPhase.NotActive:
			L.Text = "NOT ACTIVE";
			if (SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND > 50.0)
			{
				L.BackColor = Color.Red;
				L.ForeColor = Color.Black;
			}
			else if (SimulatorInformationProcessing.lastSmallInfoSimulatorData.GROUND_VELOCITY > 1.0)
			{
				L.BackColor = Color.Orange;
				L.ForeColor = Color.Black;
			}
			else
			{
				L.BackColor = Color.Black;
				L.ForeColor = Color.White;
			}
			break;
		case FlightPhase.WaitingForTakeOff:
			L.Text = "TAKE OFF";
			L.BackColor = Color.Green;
			L.ForeColor = Color.Black;
			break;
		case FlightPhase.OnGoing:
			L.Text = "ON GOING";
			L.BackColor = Color.Green;
			L.ForeColor = Color.Black;
			break;
		}
	}

	public static void SaveBuddyWorld()
	{
		DataManager.SaveObjectIntoFile(world, "BuddyWorld");
	}

	public static void QuotateAirplane()
	{
		SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.title = "";
		if (SimulatorConnectionManager.MySim != null)
		{
			SimulatorConnectionManager.Sim_RequestDataToSimConnect(SimulatorConnectionManager.DATA_REQUESTS1.REQUEST4, SimulatorConnectionManager.DEFINITIONS1.STRUCT10);
			while (SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.title == "")
			{
				Thread.Sleep(300);
				Application.DoEvents();
			}
			quotatedAirplane = new Airplane(SimulatorInformationProcessing.lastAirplaneinfoSimulatorData);
			int seed = Utility.GenerateMD5intFromString(quotatedAirplane.title + " " + DateTime.Now.DayOfYear);
			Random random = new Random(seed);
			quotatedAirplane.bodyStatus = Math.Pow(random.NextDouble(), 0.07);
			quotatedAirplane.engineStatus = Math.Pow(random.NextDouble(), 0.07);
			quotatedAirplane.registration = RandomRegistrationNumber(random);
			quotatedAirplane.quotation = 1000 * (int)(quotatedAirplane.CurrentMarketValue() * (0.95 + 0.15 * random.NextDouble()) / 1000.0);
		}
		else
		{
			MessageBox.Show("You need to be connected with the simulator");
		}
	}

	public static string RandomRegistrationNumber(Random R)
	{
		string text = "";
		string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		text += new string((from s in Enumerable.Repeat(element, 3)
			select s[R.Next(s.Length)]).ToArray());
		string element2 = "0123456789";
		return text + new string((from s in Enumerable.Repeat(element2, 2)
			select s[R.Next(s.Length)]).ToArray());
	}

	public static void BuyQuotedPlane()
	{
		string text = " (" + (100.0 * (quotatedAirplane.quotation / quotatedAirplane.CurrentMarketValue())).ToString("F1") + "% mkt value)";
		if (MessageBox.Show("Do you want to buy " + quotatedAirplane.ToString() + " for " + quotatedAirplane.quotation.ToString("F0") + text, "Airplane purchase", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			PurchaseAirplane(quotatedAirplane);
		}
	}

	public static void RentQuotedAirplane()
	{
		string text = "Do you want to rent " + quotatedAirplane.ToString() + " for " + quotatedAirplane.rentalPricePerHour().ToString("F0") + " per hour?";
		text += "(you'll pay an extra hour for every 100 miles distance from rental position when returning the plane)";
		if (MessageBox.Show(text, "Airplane rental", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			RentAirplane(quotatedAirplane);
		}
	}

	public static void SellSelectedPlane()
	{
		int seed = Utility.GenerateMD5intFromString(selectedPlane.title + " SELL " + DateTime.Now.DayOfYear);
		Random random = new Random(seed);
		selectedPlane.quotation = 1000 * (int)(selectedPlane.CurrentMarketValue() * (0.9 + 0.15 * random.NextDouble()) / 1000.0);
		string text = " (" + (100.0 * (selectedPlane.quotation / selectedPlane.CurrentMarketValue())).ToString("F1") + "% mkt value)";
		if (MessageBox.Show("Do you want to sell " + selectedPlane.ToString() + " for " + selectedPlane.quotation.ToString("F0") + text, "Airplane sale", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			SellAirplane(selectedPlane);
		}
	}

	public static void PurchaseAirplane(Airplane a)
	{
		if (!AttemptUseReputation(10.0))
		{
			MessageBox.Show("You don't have enough reputation to make a purchase");
		}
		else if (world.airplanes.Where((Airplane A) => A.registration == a.registration).Count() > 0)
		{
			MessageBox.Show("You already own this airplane!");
		}
		else if (AttemptExpense(a.quotation, "Purchase of " + a.ToString()))
		{
			world.airplanes.Add(a);
			selectedPlane = null;
			SaveBuddyWorld();
		}
		else
		{
			MessageBox.Show("Not enough money!");
		}
	}

	public static void SellAirplane(Airplane a)
	{
		if (world.airplanes.Where((Airplane A) => A.registration == a.registration).Count() == 0)
		{
			MessageBox.Show("You don't own this plane");
		}
		else if (!AttemptUseReputation(10.0))
		{
			MessageBox.Show("You don't have enough reputation to complete airplane sale.");
		}
		else if (AttemptGain(a.quotation, "Sell of " + a.ToString()))
		{
			world.airplanes.Remove(a);
			selectedPlane = null;
			SaveBuddyWorld();
		}
		else
		{
			MessageBox.Show("Bizzarre error!");
		}
	}

	public static void RentAirplane(Airplane a)
	{
		if (world.airplanes.Where((Airplane A) => A.registration == a.registration).Count() > 0)
		{
			MessageBox.Show("You already own this airplane!");
			return;
		}
		a.rented = true;
		a.rentedSince = DateTime.Now;
		a.rentedLat = a.position.latitude;
		a.rentedLon = a.position.longitude;
		world.airplanes.Add(a);
		selectedPlane = null;
		SaveBuddyWorld();
	}

	public static void ReturnPlane(Airplane a)
	{
		if (world.airplanes.Where((Airplane A) => A.registration == a.registration).Count() == 0)
		{
			MessageBox.Show("You don't have this plane as rented");
			return;
		}
		double num = 1.0 + Math.Truncate((DateTime.Now - a.rentedSince).TotalHours);
		double value = Math.Round(a.rentalPricePerHour() * num);
		ForceExpense(value, "Rental of " + a.registration + " for " + num + " hour(s)");
		double num2 = a.position.DistanceFromMiles(new WayPoint(a.rentedLat, a.rentedLon, "", "", 0.0));
		if (num2 > 5.0)
		{
			ForceExpense(Math.Round(a.rentalPricePerHour() * num2 / 100.0, 0), "Rental penalty " + a.registration + " for " + num2.ToString("F0") + "nm ");
		}
		MessageBox.Show("Airplane retuned");
		world.airplanes.Remove(a);
		selectedPlane = null;
		SaveBuddyWorld();
	}

	public static void Refuel(Airplane a, double fuelGallons)
	{
		double value = fuelGallons * 1.0;
		if (AttemptExpense(value, fuelGallons.ToString("F1") + "fuel gall for " + a.ToString()))
		{
			a.fuelgal += fuelGallons;
			SaveBuddyWorld();
		}
		else
		{
			MessageBox.Show("Not enough money!");
		}
	}

	public static void TravelTo(WayPoint Wp)
	{
		if (world.yourPosition == null || (world.yourPosition.latitude == 0.0 && world.yourPosition.longitude == 0.0))
		{
			world.yourPosition = Wp;
			MessageBox.Show("Your first travel is for free! New position set");
			SaveBuddyWorld();
			return;
		}
		double value = (int)(world.yourPosition.DistanceFromMiles(Wp) * 2.0);
		if (MessageBox.Show("Do you want to travel to " + Wp.Id + " for " + value.ToString("F0"), "Transfer to location", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			if (AttemptExpense(value, "Travel to " + Wp.Id))
			{
				world.yourPosition = Wp;
			}
			else
			{
				MessageBox.Show("Not enough money!");
			}
			SaveBuddyWorld();
		}
	}

	public static void SetAirplanePositionDescription(Airplane airplane)
	{
		OurAirport closestAirport = OurAirportsManager.GetClosestAirport(airplane.position, out var _);
		airplane.position.Id = closestAirport.ident + " - " + closestAirport.name;
	}

	public static bool AttemptExpense(double value, string description)
	{
		if (world.money > value)
		{
			world.money -= value;
			world.transactions.Add(new MoneyTransaction
			{
				value = 0.0 - value,
				date = DateTime.Now,
				description = description
			});
			return true;
		}
		return false;
	}

	public static bool ForceExpense(double value, string description)
	{
		world.money -= value;
		world.transactions.Add(new MoneyTransaction
		{
			value = 0.0 - value,
			date = DateTime.Now,
			description = description
		});
		return true;
	}

	public static bool AttemptGain(double value, string description)
	{
		world.money += value;
		world.transactions.Add(new MoneyTransaction
		{
			value = value,
			date = DateTime.Now,
			description = description
		});
		return true;
	}

	public static bool AttemptUseReputation(double reputationNeeded)
	{
		if (world.reputation >= reputationNeeded)
		{
			world.reputation -= reputationNeeded;
			return true;
		}
		return false;
	}

	public static void TryToSelectPlane(Airplane airplane)
	{
		if (flightPhase != FlightPhase.NotActive)
		{
			MessageBox.Show("You can't change plane while you are in flight");
		}
		if (CurrentLoadedPayload().Count > 0)
		{
			MessageBox.Show("You can't change plane while you have loaded payload");
		}
		else
		{
			selectedPlane = airplane;
		}
	}

	public static bool StartFlight()
	{
		if (flightPhase > FlightPhase.NotActive)
		{
			MessageBox.Show("Already flying");
			return false;
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (SimulatorConnectionManager.MySim == null)
		{
			stringBuilder.Append("You have to be connected with the simulator.\r\n");
			flightPhase = FlightPhase.NotActive;
			flightProblemDescription = stringBuilder.ToString();
			return false;
		}
		SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.title = "";
		SimulatorConnectionManager.Sim_RequestDataToSimConnect(SimulatorConnectionManager.DATA_REQUESTS1.REQUEST4, SimulatorConnectionManager.DEFINITIONS1.STRUCT10);
		while (SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.title == "")
		{
			Thread.Sleep(300);
			Application.DoEvents();
		}
		if (selectedPlane == null)
		{
			stringBuilder.Append("No airplane selected for the flight.\r\n");
		}
		else
		{
			if (selectedPlane.nextAvailableMoment > DateTime.Now)
			{
				stringBuilder.Append("This airplane is not available until " + selectedPlane.nextAvailableMoment.ToString() + ".\r\n");
			}
			if (world.yourPosition.DistanceFromMiles(selectedPlane.position) > 2.0)
			{
				stringBuilder.Append("You are not in the same location as the selected airplane.\r\n");
			}
			if (SimulatorInformationProcessing.currentAircraft.position.DistanceFromMiles(selectedPlane.position) > 2.0)
			{
				stringBuilder.Append("MSFS aircraft is not located where it should.\r\n");
			}
			if (SimulatorInformationProcessing.currentAircraft.title != selectedPlane.title)
			{
				stringBuilder.Append("You should load a " + selectedPlane.title + " but you currently are using a " + SimulatorInformationProcessing.currentAircraft.title + ".\r\n");
			}
			if (SimulatorInformationProcessing.lastSmallInfoSimulatorData.GROUND_VELOCITY > 1.0 || SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND > 30.0)
			{
				stringBuilder.Append("You have to be stopped on the ground to start the flight.\r\n");
			}
			if (selectedPlane.bodyStatus < 0.25 || selectedPlane.engineStatus < 0.25)
			{
				stringBuilder.Append("This airplane is in really bad conditions. It can't fly without maintenance.\r\n");
			}
			if (selectedPlane.bodyDamage > 0.0 || selectedPlane.engineDamage > 0.0)
			{
				stringBuilder.Append("You need to repair the airplane before flying.\r\n");
			}
			double num = Math.Abs(selectedPlane.fuelgal - SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.FUEL_TOTAL_QUANTITY);
			if (num / SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.FUEL_TOTAL_CAPACITY > 0.01 && num > 1.0)
			{
				stringBuilder.Append("Difference between fuel is above 1%.\r\n");
			}
			double num2 = SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.TOTAL_WEIGHT - SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.EMPTY_WEIGHT - SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.FUEL_TOTAL_QUANTITY_WEIGHT;
			double num3 = Math.Abs(CurrentLoadedPayloadWeight() - num2);
			if (num3 / CurrentLoadedPayloadWeight() > 0.01 && num3 > 1.0)
			{
				stringBuilder.Append("Difference between payload is above 1%.\r\n");
			}
		}
		if (stringBuilder.Length > 0)
		{
			flightPhase = FlightPhase.NotActive;
			flightProblemDescription = stringBuilder.ToString();
			return false;
		}
		flyingPlane = selectedPlane;
		flightPhase = FlightPhase.WaitingForTakeOff;
		flightProblemDescription = "";
		flightStartZuluTime = SimulatorInformationProcessing.lastBigInfoSimulatorData.zulutime;
		flightStartLocation = flyingPlane.position.Clone();
		flightEngineConsumptionTimeSeconds = 0.0;
		flightBodyConsumptionTimeSeconds = 0.0;
		return true;
	}

	public static bool EndFlight()
	{
		if ((flightPhase == FlightPhase.OnGoing || flightPhase == FlightPhase.WaitingForTakeOff) && SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_ALT_ABOVE_GROUND < 50.0 && SimulatorInformationProcessing.lastSmallInfoSimulatorData.GROUND_VELOCITY < 0.5)
		{
			if (SimulatorConnectionManager.MySim == null)
			{
				return false;
			}
			SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.title = "";
			SimulatorConnectionManager.Sim_RequestDataToSimConnect(SimulatorConnectionManager.DATA_REQUESTS1.REQUEST4, SimulatorConnectionManager.DEFINITIONS1.STRUCT10);
			while (SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.title == "")
			{
				Thread.Sleep(300);
				Application.DoEvents();
			}
			string text = "Flight completed\r\n";
			flightPhase = FlightPhase.NotActive;
			double num = SimulatorInformationProcessing.lastBigInfoSimulatorData.zulutime;
			if (num < flightStartZuluTime)
			{
				num += 86000.0;
			}
			double num2 = num - flightStartZuluTime;
			text = text + "Duration: " + new TimeSpan(0, 0, (int)num2).ToString() + "\r\n";
			flyingPlane.flightHours += num2 / 3600.0;
			flyingPlane.completeFlights++;
			flyingPlane.BodyConsumption(flightBodyConsumptionTimeSeconds);
			flyingPlane.EngineConsumption(flightBodyConsumptionTimeSeconds);
			flightEngineConsumptionTimeSeconds = 0.0;
			flightBodyConsumptionTimeSeconds = 0.0;
			double latitude = SimulatorInformationProcessing.lastSmallInfoSimulatorData.latitude;
			double longitude = SimulatorInformationProcessing.lastSmallInfoSimulatorData.longitude;
			double altitude = SimulatorInformationProcessing.lastSmallInfoSimulatorData.altitude;
			WayPoint wayPoint = new WayPoint(latitude, longitude, "???", "landing position", altitude);
			OurAirport closestAirport = OurAirportsManager.GetClosestAirport(wayPoint, out var mindistance);
			if (mindistance < 2.0)
			{
				wayPoint.Id = closestAirport.ident;
			}
			else
			{
				wayPoint.Id = "Unkown location";
			}
			text = text + "Landed at: " + wayPoint.Id + "\r\n";
			double num3 = wayPoint.DistanceFromMiles(flightStartLocation);
			flyingPlane.flightMileage += num3;
			text = text + "Distance: " + num3.ToString("F0") + " nm\r\n";
			world.yourPosition = wayPoint;
			flyingPlane.position = wayPoint;
			flyingPlane.fuelgal = SimulatorInformationProcessing.lastAirplaneinfoSimulatorData.FUEL_TOTAL_QUANTITY;
			flyingPlane.heading = SimulatorInformationProcessing.lastSmallInfoSimulatorData.PLANE_HEADING_RADIANT_TRUE;
			MessageBox.Show(text);
			if (DateTime.Now - world.lastDamageChecking > new TimeSpan(24, 0, 0))
			{
				Random random = new Random(DateTime.Now.Millisecond * DateTime.Now.DayOfYear + Utility.GenerateMD5intFromString("BODY"));
				if (random.NextDouble() > Math.Pow(flyingPlane.bodyStatus, 0.25))
				{
					flyingPlane.bodyDamage = Utility.Clamp(random.NextDouble() * 2.0 * (1.0 - flyingPlane.bodyStatus), 0.0, 1.0);
					MessageBox.Show(flyingPlane.registration + " is damaged and won't be able to fly again without reparation");
				}
				Random random2 = new Random(DateTime.Now.Millisecond * DateTime.Now.DayOfYear + Utility.GenerateMD5intFromString("ENGINE"));
				if (random2.NextDouble() > Math.Pow(flyingPlane.engineStatus, 0.25))
				{
					flyingPlane.engineDamage = Utility.Clamp(random2.NextDouble() * 2.0 * (1.0 - flyingPlane.engineStatus), 0.0, 1.0);
					MessageBox.Show(flyingPlane.registration + " engine is damaged and won't be able to work again without reparation");
				}
				world.lastDamageChecking += new TimeSpan(24, 0, 0);
			}
			SaveBuddyWorld();
			flyingPlane = null;
			flightStartZuluTime = 0.0;
			flightStartLocation = null;
			NotifyRefresh(null, EventArgs.Empty);
			return true;
		}
		return false;
	}

	public static void AbortFlight()
	{
		flightPhase = FlightPhase.NotActive;
		foreach (Activity activity in world.activities)
		{
			foreach (PayLoad payload in activity.payloads)
			{
				payload.Loaded = false;
			}
		}
		SaveBuddyWorld();
	}

	public static void AttemptBodyMaintenanceOrRepair(Airplane a)
	{
		if (a.nextAvailableMoment > DateTime.Now)
		{
			throw new Exception("This airplane is not available until " + selectedPlane.nextAvailableMoment.ToString() + ".");
		}
		if (flightPhase > FlightPhase.NotActive)
		{
			throw new Exception("Currently in flight.");
		}
		if (a.bodyDamage == 0.0)
		{
			BodyMainteanance(a);
		}
		else
		{
			BodyRepair(a);
		}
	}

	public static void AttemptEngineMaintenanceOrRepair(Airplane a)
	{
		if (a.nextAvailableMoment > DateTime.Now)
		{
			throw new Exception("This airplane is not available until " + selectedPlane.nextAvailableMoment.ToString() + ".");
		}
		if (flightPhase > FlightPhase.NotActive)
		{
			throw new Exception("Currently in flight.");
		}
		if (a.engineDamage == 0.0)
		{
			EngineMainteanance(a);
		}
		else
		{
			EngineRepair(a);
		}
	}

	public static void BodyMainteanance(Airplane a)
	{
		double num = 0.75 * (1.0 - a.bodyStatus);
		if (num < 0.01)
		{
			MessageBox.Show("This airplane does not need any maintenance");
			return;
		}
		double num2 = Utility.CubizeValue(5, a.BodyMaintenanceCost(num));
		if (MessageBox.Show($"Do you want to perform body maintenance for {num2}", "Mainteanance", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			if (AttemptExpense(num2, "Body maintenance " + a.registration))
			{
				a.bodyStatus = Utility.Clamp(a.bodyStatus + num, 0.0, 1.0);
				a.nextAvailableMoment = DateTime.Now + new TimeSpan(0, (int)(2500.0 * Math.Log(num + 1.0)), 0);
				MessageBox.Show("Body maintenance in progress. Airplane will be available at " + a.nextAvailableMoment.ToString());
			}
			SaveBuddyWorld();
		}
	}

	public static void EngineMainteanance(Airplane a)
	{
		double num = 0.75 * (1.0 - a.engineStatus);
		if (num < 0.01)
		{
			MessageBox.Show("This airplane does not need any maintenance");
			return;
		}
		double num2 = Utility.CubizeValue(5, a.EngineMaintenanceCost(num));
		if (MessageBox.Show($"Do you want to perform engine maintenance for {num2}", "Mainteanance", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			if (AttemptExpense(num2, "Engine maintenance " + a.registration))
			{
				a.engineStatus = Utility.Clamp(a.engineStatus + num, 0.0, 1.0);
				a.nextAvailableMoment = DateTime.Now + new TimeSpan(0, (int)(2500.0 * Math.Log(num + 1.0)), 0);
				MessageBox.Show("Engine maintenance in progress. Airplane will be available at " + a.nextAvailableMoment.ToString());
			}
			SaveBuddyWorld();
		}
	}

	public static void BodyRepair(Airplane a)
	{
		double bodyDamage = a.bodyDamage;
		double num = Utility.CubizeValue(5, a.BodyRepairCost(bodyDamage));
		if (MessageBox.Show($"Do you want to repair body airplane for {num}", "Repair", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			if (AttemptExpense(num, "Body repair" + a.registration))
			{
				a.bodyStatus = Utility.Clamp(a.bodyStatus + a.bodyDamage * 0.5, 0.0, 1.0);
				a.nextAvailableMoment = DateTime.Now + new TimeSpan(0, (int)(2500.0 * Math.Log(a.bodyDamage + 1.0)), 0);
				a.bodyDamage = 0.0;
				MessageBox.Show("Body repair in progress. Airplane will be available at " + a.nextAvailableMoment.ToString());
			}
			SaveBuddyWorld();
		}
	}

	public static void EngineRepair(Airplane a)
	{
		double engineDamage = a.engineDamage;
		double num = Utility.CubizeValue(5, a.EngineRepairCost(engineDamage));
		if (MessageBox.Show($"Do you want to repair Engine airplane for {num}", "Repair", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			if (AttemptExpense(num, "Engine repair" + a.registration))
			{
				a.engineStatus = Utility.Clamp(a.engineStatus + a.engineDamage * 0.5, 0.0, 1.0);
				a.nextAvailableMoment = DateTime.Now + new TimeSpan(0, (int)(2500.0 * Math.Log(a.engineDamage + 1.0)), 0);
				a.engineDamage = 0.0;
				MessageBox.Show("Engine repair in progress. Airplane will be available at " + a.nextAvailableMoment.ToString());
			}
			SaveBuddyWorld();
		}
	}

	public static void TryToLoad(PayLoad payload)
	{
		if (flightPhase != FlightPhase.NotActive)
		{
			MessageBox.Show("You can't load while in flight");
			return;
		}
		if (selectedPlane == null)
		{
			MessageBox.Show("You have to select an airplane");
			return;
		}
		if (selectedPlane.position.DistanceFromMiles(payload.Position) > 2.0)
		{
			MessageBox.Show("You are too far from payload to load it");
			return;
		}
		if (payload.Loaded)
		{
			MessageBox.Show("Payload is already on board");
			return;
		}
		if (!world.activities.Where((Activity A) => A.payloads.Contains(payload)).FirstOrDefault().Accepted)
		{
			MessageBox.Show("You have to accept activity first");
			return;
		}
		payload.Load();
		NotifyRefresh(null, EventArgs.Empty);
	}

	public static void TryToUnload(PayLoad payload)
	{
		if (flightPhase != FlightPhase.NotActive)
		{
			MessageBox.Show("You can't load while in flight");
			return;
		}
		if (selectedPlane == null)
		{
			MessageBox.Show("You have to select an airplane");
			return;
		}
		if (!payload.Loaded)
		{
			MessageBox.Show("Payload is not on board");
			return;
		}
		payload.Unload(selectedPlane.position.Clone(), world.activities.Where((Activity A) => A.payloads.Contains(payload)).FirstOrDefault());
		NotifyRefresh(null, EventArgs.Empty);
		SaveBuddyWorld();
	}

	public static List<PayLoad> CurrentLoadedPayload()
	{
		List<PayLoad> list = new List<PayLoad>();
		foreach (Activity activity in world.activities)
		{
			list.AddRange(activity.payloads.Where((PayLoad p) => p.Loaded).ToList());
		}
		return list;
	}

	public static double CurrentLoadedPayloadWeight()
	{
		return (double)world.pilotWeight + CurrentLoadedPayload().Sum((PayLoad P) => P.WeightLb);
	}

	public static void CleanupExpiredActivities()
	{
		world.activities.RemoveAll((Activity A) => A.Expired());
	}

	public static void CleanupLoadedPayload()
	{
		foreach (Activity activity in world.activities)
		{
			activity.payloads.ForEach(delegate(PayLoad p)
			{
				p.Loaded = false;
			});
		}
	}

	public static void RequestNewActivity(WayPoint NewActivityPosition)
	{
		double reputationNeeded = 5.0;
		if (NewActivityPosition.Id == world.homeBase)
		{
			reputationNeeded = 2.0;
		}
		if (!AttemptUseReputation(reputationNeeded))
		{
			MessageBox.Show("You don't have enough reputation to generate new activities");
			return;
		}
		Activity activity;
		for (activity = null; activity == null; activity = Activity.GenerateActivity(NewActivityPosition))
		{
		}
		world.activities.Add(activity);
		SaveBuddyWorld();
	}

	public static void TryToAdvertise()
	{
		int num = (int)(1.0 + world.reputation * 0.333);
		double num2 = Utility.CubizeValue(10, 10.0 * Math.Pow(num, 1.5));
		if (MessageBox.Show($"Do you want to make an advertisement campaign to get +{num} reputation for a cost of {num2}$", "Advertisment campaign", MessageBoxButtons.YesNo) == DialogResult.Yes && AttemptExpense(num2, "Advertisement campaign"))
		{
			world.reputation += num;
		}
	}

	public static void TryToSetHome(string newHomeBaseIcao)
	{
		try
		{
			OurAirport airport = OurAirportsManager.GetAirport(newHomeBaseIcao);
			if (airport == null)
			{
				throw new Exception("Provide a valid ICAO code for new home base");
			}
			if (newHomeBaseIcao == (world.homeBase ?? ""))
			{
				throw new Exception("Provide a new ICAO code for new home base");
			}
			if (MessageBox.Show("Are you sure you want to transfer your home base to " + newHomeBaseIcao + " for 10000$ and half of your reputation?", "Home base transfer", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if (!AttemptExpense(10000.0, "Transfer home base to " + newHomeBaseIcao))
				{
					throw new Exception("You don't have enough money");
				}
				world.reputation = Math.Truncate(world.reputation * 0.5);
				world.homeBase = airport.ident;
				SaveBuddyWorld();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public static void RaiseEventNotifyRefreshActivities()
	{
		NotifyRefreshActivities(null, EventArgs.Empty);
	}
}
