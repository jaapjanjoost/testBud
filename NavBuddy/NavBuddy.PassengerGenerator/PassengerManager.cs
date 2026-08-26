using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NavBuddy.PassengerGenerator;

public static class PassengerManager
{
	public const string ANAGRAPHICAL_RESOURCE_PATH = "anagraphical resources\\";

	private static Dictionary<string, long> filesAvailability = new Dictionary<string, long>();

	public static Passenger GenerateRandomPassenger(Random RND, string country)
	{
		int age = 18 + (int)Utility.dado(RND, 20L) + (int)Utility.dado(RND, 20L);
		string text = "";
		text = ((!Utility.chance(RND, 0.5)) ? "m" : "f");
		return GenerateRandomPassenger(RND, country, age, text);
	}

	public static Passenger GenerateRandomPassenger(Random RND, string country, int Age)
	{
		string text = "";
		text = ((!Utility.chance(RND, 0.5)) ? "m" : "f");
		return GenerateRandomPassenger(RND, country, Age, text);
	}

	public static Passenger GenerateRandomPassenger(Random RND, string country, string Gender)
	{
		int age = 18 + (int)Utility.dado(RND, 20L) + (int)Utility.dado(RND, 20L);
		return GenerateRandomPassenger(RND, country, age, Gender);
	}

	public static Passenger GenerateRandomPassenger(Random RND, string country, int Age, string Gender)
	{
		Passenger passenger = new Passenger();
		passenger.Country = country;
		passenger.Age = Age;
		passenger.Gender = Gender;
		passenger.Name = firstName(RND, country, passenger.Gender);
		passenger.Surname = surName(RND, country);
		if (passenger.Age >= 18)
		{
			if (18 + Utility.dado(RND, 12L) >= passenger.Age)
			{
				passenger.Profession = "Student";
			}
			else if (60 + Utility.dado(RND, 10L) <= passenger.Age)
			{
				passenger.Profession = "Retired";
			}
			else if (Utility.chance(RND, 0.06))
			{
				passenger.Profession = "Unemployed";
			}
			else
			{
				passenger.Profession = professionSelection(RND);
			}
		}
		else
		{
			passenger.Profession = "";
		}
		double num = 50.0 + RND.NextDouble() * 40.0;
		if (passenger.Gender == "f")
		{
			num *= 0.8;
		}
		if (passenger.Age < 15)
		{
			num = 5.0 + (num - 5.0) / 15.0 * (double)passenger.Age;
		}
		else if (RND.NextDouble() < 0.1)
		{
			num += RND.NextDouble() * 30.0;
		}
		passenger.WeightPounds = Math.Round(num / 0.453592, 1);
		return passenger;
	}

	public static Passenger GenerateSpecificPassenger(Random RND, string country, int Age, string Gender, string Name, string Surname, string Profession, double RandomAdultWeightKg)
	{
		Passenger passenger = new Passenger();
		passenger.Country = country;
		passenger.Age = Age;
		passenger.Gender = Gender;
		passenger.Name = Name;
		passenger.Surname = Surname;
		passenger.Profession = Profession;
		if (passenger.Gender == "f")
		{
			RandomAdultWeightKg *= 0.8;
		}
		passenger.WeightPounds = Math.Round(RandomAdultWeightKg / 0.453592, 1);
		return passenger;
	}

	public static string firstName(Random RND, string country, string gender)
	{
		string text = "anagraphical resources\\names_" + country + "_" + gender;
		if (!File.Exists(text) || RND.NextDouble() < 0.1)
		{
			text = RandomCountryNameFile(gender, RND);
		}
		if (!filesAvailability.ContainsKey(text))
		{
			filesAvailability.Add(text, CountLinesLINQ(new FileInfo(text)));
		}
		long line = Utility.dado(RND, filesAvailability[text]);
		return ToTitleCase(GetLineFromFile(new FileInfo(text), line));
	}

	public static string surName(Random RND, string country)
	{
		string text = "anagraphical resources\\surnames_" + country;
		if (!File.Exists(text) || RND.NextDouble() < 0.1)
		{
			text = RandomCountrySurnameFile(RND);
		}
		if (!filesAvailability.ContainsKey(text))
		{
			filesAvailability.Add(text, CountLinesLINQ(new FileInfo(text)));
		}
		long line = Utility.dado(RND, filesAvailability[text]);
		return ToTitleCase(GetLineFromFile(new FileInfo(text), line));
	}

	public static char RandomUppercaseLetter(Random RND)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		int index = RND.Next(0, text.Length);
		return text[index];
	}

	public static string CompanyName(Random RND)
	{
		string text = "";
		if (RND.NextDouble() < 0.5)
		{
			string text2 = RandomCountrySurnameFile(RND);
			if (!filesAvailability.ContainsKey(text2))
			{
				filesAvailability.Add(text2, CountLinesLINQ(new FileInfo(text2)));
			}
			long line = Utility.dado(RND, filesAvailability[text2]);
			text = GetLineFromFile(new FileInfo(text2), line);
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				text += RandomUppercaseLetter(RND);
			}
		}
		text = RND.Next(3) switch
		{
			0 => text + " inc", 
			1 => text + " ltd", 
			2 => text + " international", 
			3 => text + " &C", 
			_ => text + " company", 
		};
		return RND.Next(2) switch
		{
			0 => ToTitleCase(text), 
			1 => text.ToLower(), 
			_ => text.ToUpper(), 
		};
	}

	private static string RandomCountryNameFile(string gender, Random RND)
	{
		string[] array = (from filename in Directory.GetFiles("anagraphical resources\\")
			where filename.StartsWith("anagraphical resources\\names_") && filename.EndsWith("_" + gender)
			select filename).ToArray();
		return array[RND.Next(array.Count())];
	}

	private static string RandomCountrySurnameFile(Random RND)
	{
		string[] array = (from filename in Directory.GetFiles("anagraphical resources\\")
			where filename.StartsWith("anagraphical resources\\surnames_")
			select filename).ToArray();
		return array[RND.Next(array.Count())];
	}

	private static string professionSelection(Random RND)
	{
		string text = "anagraphical resources\\professions";
		if (!File.Exists(text))
		{
			return "[NO FILE " + text + "]";
		}
		if (!filesAvailability.ContainsKey(text))
		{
			filesAvailability.Add(text, CountLinesLINQ(new FileInfo(text)));
		}
		long line = Utility.dado(RND, filesAvailability[text]);
		return GetLineFromFile(new FileInfo(text), line);
	}

	private static string ToTitleCase(string sentence)
	{
		string text = "";
		sentence = sentence.Trim();
		char[] array = sentence.ToCharArray();
		text += array[0].ToString().ToUpper();
		for (int i = 1; i < array.Length; i++)
		{
			text = ((array[i - 1] != ' ' && array[i - 1] != '\'') ? (text + array[i].ToString().ToLower()) : (text + array[i].ToString().ToUpper()));
		}
		return text;
	}

	public static long CountLinesLINQ(FileInfo file)
	{
		return File.ReadLines(file.FullName).Count();
	}

	public static string GetLineFromFile(FileInfo file, long line)
	{
		return File.ReadAllLines(file.FullName, Encoding.Default)[line];
	}
}
