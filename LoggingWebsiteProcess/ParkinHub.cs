using Microsoft.AspNetCore.SignalR;

namespace LoggingWebsiteProcess
{
    public class ParkinHub : Hub
    {
        public async Task SendUserData(object parkingDetails)
        {
            await Clients.All.SendAsync("ReceiveUserData", parkingDetails);
        }
    }
}
