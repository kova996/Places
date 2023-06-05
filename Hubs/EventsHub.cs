using Microsoft.AspNetCore.SignalR;
using Places.Models;

namespace Places.Hubs
{
    public class EventsHub : Hub
    {
        public async Task SendMessage(FoursquareRequest message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}