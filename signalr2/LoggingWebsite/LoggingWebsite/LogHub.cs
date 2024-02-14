using Microsoft.AspNetCore.SignalR;

namespace LoggingWebsite
{
    public class LogHub :Hub
    {
        public async Task SendMessage( string message)
        {
            // Broadcast the received message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
