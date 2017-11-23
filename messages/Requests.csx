#r "System.Net.Http"
#r "System.Web"
#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

public class Requests
{
    private static readonly HttpClient client = new HttpClient();

    public async static Task<string> SearchEventBrite(string search)
	{
        const string token = "MDBBLI6SHELI6ARWNURT";
	    string search_encoded = HttpUtility.UrlEncode(search);
	    string url = $"https://www.eventbriteapi.com/v3/events/search/?q={search_encoded}&token={token}";

	    string response = await client.GetStringAsync(url);

        var result = JsonConvert.DeserializeObject<dynamic>(response);

	    return result["events"];

	}
}
	