using Microsoft.AspNetCore.SignalR;

namespace LoggingWebsiteProcess
{
    public class LogHub :Hub
    {
        public async Task SendProcessInfo(string processInfo)
        {
            // Broadcast the received process info to all connected clients
            await Clients.All.SendAsync("ReceiveProcessInfo", processInfo);
        }
    }
}
