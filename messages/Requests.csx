#r "System.Net.Http"
#r "System.Web"
#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

public class CalendarEntry
{
	public string name {get; set;}
	public string start {get; set;}
	public string end {get; set;}
	public string location {get; set;}

}

public class Requests
{
	const string general_token = "MDBBLI6SHELI6ARWNURT";
	const string venue_token = "M7JQH466CX3WQOU72CSZ ";
    private static readonly HttpClient client = new HttpClient();
    public async static Task<string> SearchEventBrite(string search)
	{
	    string search_encoded = HttpUtility.UrlEncode(search);
	    string url = $"https://www.eventbriteapi.com/v3/events/search/?q={search_encoded}&token={general_token}";

	    string response = await client.GetStringAsync(url);

        var result = JsonConvert.DeserializeObject<dynamic>(response);

	    return result["events"];

	}

	public async static Task<string> GetVenueAddress(string venueID) {
		string url = $"https://www.eventbriteapi.com/v3/venues/{venueID}/?token={general_token}";

		string response = await client.GetStringAsync(url);

		var result = JsonConvert.DeserializeObject<dynamic>(response);

		return result;
	}

	public async static Task<bool> SetCalendarEntry(string name, DateTime start, DateTime end, string location) {
		string officeURL = "https://prod-15.westeurope.logic.azure.com:443/workflows/22c10836f56e4a9ca58759449d98bc54/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Aq0oTX4UhhRxi6aslD1xZVOtSznoLhKIBQsEFEJKAVY";
		
		var format = "YYYY-MM-ddTHH:mm:00";
		/*var entry = new CalendarEntry();
		/*entry.name = name;
		entry.start = start.ToString(format) ?? "";
		entry.end = end.ToString(format) ?? "";
		entry.location = location.ToString() ?? "";

		var json = JsonConvert.SerializeObject(entry);

		var response = await client.PostAsync(officeURL, new StringContent(json, Encoding.UTF8, "application/json"));

		return response.StatusCode.ToString() == "200";*/
	
	}
}
