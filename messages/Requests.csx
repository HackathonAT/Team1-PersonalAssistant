#r "System.Net.Http"
#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

public class Requests
{
    private static readonly System.Net.HttpClient client = new HttpClient();

    public static string SearchEventBrite(string search)
	{
        const string token = "MDBBLI6SHELI6ARWNURT";
	    string search_encoded = HttpUtily.UrlEncode(search);
	    string url = $"https://www.eventbriteapi.com/v3/events/search/?q={search_encoded}&token={token}";

	    string response = await client.GetStringAsync(url);

        var result = JsonConvert.DeserializeObject<dynamic>(response);

	    return result["events"][0]["name"]["text"];

	}
}
