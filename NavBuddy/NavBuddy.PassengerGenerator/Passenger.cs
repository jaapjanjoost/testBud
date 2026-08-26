namespace NavBuddy.PassengerGenerator;

public class Passenger
{
	public int Age { get; set; }

	public string Name { get; set; }

	public string Surname { get; set; }

	public string Gender { get; set; }

	public string Profession { get; set; }

	public double WeightPounds { get; set; }

	public string Country { get; set; }

	public string Description()
	{
		return $"{Name} {Surname} {Gender}[{Age}] - {Country} - {Profession}";
	}
}
