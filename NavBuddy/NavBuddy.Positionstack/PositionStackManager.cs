using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using NavBuddy.BuddyWorld;
using Newtonsoft.Json;

namespace NavBuddy.Positionstack;

public static class PositionStackManager
{
	private static PositionStackData lastPositionStackData = null;

	private static async Task ReverseLocation_Call(double latitude, double longitude)
	{
		HttpClient client = new HttpClient();
		CultureInfo invC = CultureInfo.InvariantCulture;
		string strLatitude = latitude.ToString("G", invC);
		string strLongitude = longitude.ToString("G", invC);
		string http = "http";
		string key = BuddyWorldManager.world.positionstackkey;
		string uri = http + "://api.positionstack.com/v1/reverse?access_key=" + key + "&query=" + strLatitude + "," + strLongitude;
		HttpResponseMessage result = await client.GetAsync(uri);
		Console.WriteLine(result.StatusCode);
		string contents = await result.Content.ReadAsStringAsync();
		Console.WriteLine(contents);
		lastPositionStackData = JsonConvert.DeserializeObject<PositionStackData>(contents, new JsonSerializerSettings
		{
			MissingMemberHandling = MissingMemberHandling.Ignore
		});
	}

	public static PositionStackData ReverseLocation(WayPoint WP)
	{
		lastPositionStackData = null;
		Task task = Task.Run(async delegate
		{
			await ReverseLocation_Call(WP.latitude, WP.longitude);
		});
		task.Wait(5000);
		return lastPositionStackData;
	}
}
