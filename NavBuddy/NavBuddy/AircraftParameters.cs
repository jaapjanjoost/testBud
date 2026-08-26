namespace NavBuddy;

public class AircraftParameters : ISalvableDataObject
{
	public double SafeIas { get; set; } = 0.0;

	public double LandingIas { get; set; } = 0.0;

	public double ClimbIas { get; set; } = 0.0;

	public double CruiseIas { get; set; } = 0.0;

	public double DescIas { get; set; } = 0.0;

	public double ClimbFPM { get; set; } = 0.0;

	public double DescFPM { get; set; } = 0.0;

	public double LandFPM { get; set; } = -100.0;

	public double TouchFPM { get; set; } = 0.0;

	public double TakeOffCompletedAGL { get; set; } = 0.0;

	public double RunwayEntAGL { get; set; } = 0.0;

	public double LandingGearUpAGL { get; set; } = 0.0;

	public double LandingGearDownAGL { get; set; } = 0.0;

	public double FlapsTakeOffPerc { get; set; } = 25.0;

	public double FlapsLandingPerc { get; set; } = 100.0;

	public double FlapsTakeOffIas { get; set; } = 80.0;

	public double FlapsLandingIas { get; set; } = 120.0;

	public double RudderEffect { get; set; } = 0.0;

	public double RudderDamper { get; set; } = 0.0;

	public double AileronEffect { get; set; } = 0.0;

	public double AileronDamper { get; set; } = 0.0;

	public double ElevatorEffect { get; set; } = 100.0;

	public double ElevatorDamper { get; set; } = 100.0;

	public double ThrottleEffect { get; set; } = 100.0;

	public double ThrottleDamper { get; set; } = 100.0;

	public double FlareAngle { get; set; } = 3.0;

	public double MaxBankAngle { get; set; } = 30.0;

	public double ReverseThrust { get; set; } = 0.0;

	string ISalvableDataObject.FileIdentifier()
	{
		return "AircraftParameters";
	}
}
