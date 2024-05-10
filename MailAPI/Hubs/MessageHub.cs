using Microsoft.AspNetCore.SignalR;

namespace MailAPI.Hubs
{
    public class MessageHub:Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage",message);
        }
    }
}
