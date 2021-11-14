using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace publisher.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
               await Clients.All.SendAsync("receieveMessage",message);
        }
    }
}