using System.Linq;

namespace NavBuddy.BuddyWorld;

public class PayLoad
{
	public const string TYPE_PASSENGER = "PASSENGER";

	public const string TYPE_CARGO = "CARGO";

	public bool tmpReached1000FeetAGL = false;

	public Activity tmpActivity = null;

	public string Type { get; set; }

	public string Description { get; set; }

	public double WeightLb { get; set; }

	public WayPoint Position { get; set; }

	public WayPoint Destination { get; set; }

	public bool Loaded { get; set; } = false;

	public bool Delivered { get; set; } = false;

	public void Load()
	{
		Loaded = true;
		tmpActivity = BuddyWorldManager.world.activities.Where((Activity A) => A.payloads.Contains(this)).FirstOrDefault();
	}

	public void Unload(WayPoint newposition, Activity activity)
	{
		Loaded = false;
		Position = newposition;
		if (Destination != null && newposition.DistanceFromMiles(Destination) < 2.0)
		{
			Deliver();
		}
	}

	public void Deliver()
	{
		Delivered = true;
		if (tmpActivity == null)
		{
			tmpActivity = BuddyWorldManager.world.activities.Where((Activity A) => A.payloads.Contains(this)).FirstOrDefault();
		}
		tmpActivity.PayloadDelivered(this);
	}

	public double PathHeading()
	{
		if (Position != null && Destination != null)
		{
			return Position.CurrentBearingDegree(Destination);
		}
		return -1.0;
	}

	public double PathMiles()
	{
		if (Position != null && Destination != null)
		{
			return Position.DistanceFromMiles(Destination);
		}
		return 0.0;
	}
}
