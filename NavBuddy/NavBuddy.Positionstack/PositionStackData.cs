using System.Linq;

namespace NavBuddy.Positionstack;

public class PositionStackData
{
	public PositionStackElement[] data { get; set; }

	public WayPoint PossibleValidPosition()
	{
		if (data == null)
		{
			return null;
		}
		if (data.Count() == 0)
		{
			return null;
		}
		if (data.Where((PositionStackElement D) => D.type.ToLower() == "marinearea").Count() > 0)
		{
			return null;
		}
		PositionStackElement[] array = data.Where((PositionStackElement D) => D.type.ToLower() != "region" && D.type.ToLower() != "marinearea" && D.type.ToLower() != "ocean").ToArray();
		if (array.Count() == 0)
		{
			return null;
		}
		return new WayPoint(array[0].latitude, array[0].longitude, array[0].name, array[0].type, 0.0);
	}
}
