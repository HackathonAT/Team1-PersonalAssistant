#load "Requests.csx"

using System;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

// For more information about this template visit http://aka.ms/azurebots-csharp-luis
[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    //public TimeSpan hour = new TimeSpan(36, 0, 0, 0);
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
    {
    }

    [LuisIntent("")]
    [LuisIntent("None")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"Sorry, I didn't quite get that! Please rephrase your request."); //
        //await context.PostAsync(await Requests.SearchEventBrite("WeAreDevelopers"));
        context.Wait(MessageReceived);
    }

    // Go to https://luis.ai and create a new intent, then train/publish your luis app.
    // Finally replace "MyIntent" with the name of your newly created intent in the following handler
    [LuisIntent("Events.Book")]
    public async Task EventBookIntent(IDialogContext context, LuisResult result)
    {
        LuisResult name; 
    
        if (result.TryFindEntity("Events.Name", out name)) {
            context.PostAsync(name["entity"]);
            //var eventResult = await Requests.SearchEventBrite(name["entity"]);
            /*if (eventResult.Length == 0) {
                await context.PostAsync("Sorry! I couldn't find this event!");
            } else {
                context.UserData["eventResult"] = eventResult;
                if (!context.UserData.ContainsKey("searchIndex")) {
                    context.UserData["searchIndex"] = 0;
                } else {
                    context.UserData["searchIndex"]++;
                }
                var idx = context.UserData["searchIndex"];
                var nameDB = eventResult[idx]["name"]["text"];
                var startUTC = DateTime.Parse(eventResult[idx]["start"]["utc"]);
                var start = startUTC.Add(hour);
                //var end =eventResultresult[0]["end"]["utc"];
                await context.PostAsync($"Do you mean {nameDB} starting at {start.ToString()}?");
                context.UserData["expectingYesNo"] = 1;
            }*/
        } else {
            await context.PostAsync($"You didn't provide a name!");            
        }
        context.Wait(MessageReceived);
    }

    /*[LuisIntent("Utilities.Confirm")]
    public async Task UtilitiesConfirmIntent(IDialogContext context, LuisResult result)
    {
        if (context.UserData["expectingYesNo"] == 1) {
            var eventResult = context.UserData["eventResult"];
            var startUTC = DateTime.Parse(eventResult["start"]["utc"]);
            var start = startUTC.Add(hour);
            var endUTC = DateTime.Parse(eventResult["start"]["utc"]);
            var end = endUTC.Add(hour);
            var venueID = eventResult["venue_id"];
            var venue = Requests.GetVenueAddress(venueID)["address"]["localized_address_display"];
            if (Requests.SetCalendarEntry(eventResult["name"]["text"], start, end, venue)) {
                await context.PostAsync("The event has been submitted to your calendar successfully!");
            } else {
                await context.PostAsync("There was an error editing your calendar!");                
            }
        } else {
            await context.PostAsync("I didn't ask you a Yes/No question!");
        }
    }*/
}