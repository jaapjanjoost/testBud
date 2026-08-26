using System;

public static class StandardFormatter
{
	public static string FormatTimeSpan(TimeSpan t)
	{
		try
		{
			DateTime dateTime = new DateTime(t.Ticks);
			return dateTime.ToString("HH") + "h " + dateTime.ToString("mm") + "m";
		}
		catch
		{
			return "ERR";
		}
	}

	public static string FormatTimeSpanWithSeconds(TimeSpan t)
	{
		try
		{
			DateTime dateTime = new DateTime(t.Ticks);
			return dateTime.ToString("HH") + "h " + dateTime.ToString("mm") + "m " + dateTime.ToString("ss") + "s";
		}
		catch
		{
			return "ERR";
		}
	}

	public static string FormatDateTime(DateTime d)
	{
		try
		{
			return d.ToString("HH:mm");
		}
		catch
		{
			return "ERR";
		}
	}

	public static string FormatDateTimeWithSeconds(DateTime d)
	{
		try
		{
			return d.ToString("HH:mm:ss");
		}
		catch
		{
			return "ERR";
		}
	}
}
