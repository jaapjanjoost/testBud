using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NavBuddy;

public static class Utility
{
	public static bool chance(Random RND, double probability)
	{
		return RND.NextDouble() < probability;
	}

	public static long dado(Random RND, long faces)
	{
		return (long)Math.Floor(RND.NextDouble() * (double)faces);
	}

	public static double RadToDeg(double rad)
	{
		return 180.0 / Math.PI * rad;
	}

	public static double DegToRad(double deg)
	{
		return Math.PI / 180.0 * deg;
	}

	public static double NorthRelatedDegToCartesianDeg(double northDeg)
	{
		return NormalizeAngleDegree(90.0 - northDeg);
	}

	public static double NorthRelatedRadToCartesianRad(double northRad)
	{
		return NormalizeAngleRadiant(Math.PI / 2.0 - northRad);
	}

	public static double NormalizeAngleDegree(double degreeAngle)
	{
		return (degreeAngle > 360.0) ? (degreeAngle - 360.0) : degreeAngle;
	}

	public static double NormalizeAngleRadiant(double radAngle)
	{
		return (radAngle > Math.PI * 2.0) ? (radAngle - Math.PI * 2.0) : radAngle;
	}

	public static double MinimizeAngleDegree(double degreeAngle)
	{
		if (degreeAngle > 180.0)
		{
			return degreeAngle - 360.0;
		}
		if (degreeAngle < -180.0)
		{
			return degreeAngle + 360.0;
		}
		return degreeAngle;
	}

	public static double MinimizeAngleRad(double radAngle)
	{
		if (radAngle > Math.PI)
		{
			return radAngle - Math.PI * 2.0;
		}
		if (radAngle < -Math.PI)
		{
			return radAngle + Math.PI * 2.0;
		}
		return radAngle;
	}

	public static double Clamp(double value, double min, double max)
	{
		if (value > max)
		{
			return max;
		}
		if (value < min)
		{
			return min;
		}
		return value;
	}

	public static int Clamp(int value, int min, int max)
	{
		if (value > max)
		{
			return max;
		}
		if (value < min)
		{
			return min;
		}
		return value;
	}

	public static double toDouble(string internationalDoubleString)
	{
		internationalDoubleString = internationalDoubleString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
		return double.Parse(internationalDoubleString);
	}

	public static string FromDouble(double? value)
	{
		if (!value.HasValue)
		{
			return "";
		}
		return value.ToString().Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
	}

	public static double? TryToDouble(string internationalDoubleString)
	{
		internationalDoubleString = internationalDoubleString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
		if (double.TryParse(internationalDoubleString, out var result))
		{
			return result;
		}
		return null;
	}

	public static string RemoveQuotes(string quotedstring)
	{
		return quotedstring.Replace("\"", "").Trim();
	}

	public static string AddQuotes(string unquotedstring)
	{
		return "\"" + unquotedstring + "\"";
	}

	public static int GenerateMD5intFromString(string input)
	{
		using MD5 mD = MD5.Create();
		byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input));
		Guid guid = new Guid(array);
		int num = 1;
		for (int i = 0; i < array.Length; i++)
		{
			num = num * array[i] % int.MaxValue;
		}
		return num;
	}

	public static int CubizeValue(int unit, double value)
	{
		return (int)((double)unit * (1.0 + Math.Floor(value / (double)unit)));
	}

	public static T RandomListSelector<T>(List<T> lista, Random RND)
	{
		if (lista.Count > 0)
		{
			return lista[RND.Next(lista.Count())];
		}
		return default(T);
	}
}
