using Microsoft.AspNetCore.SignalR;
using SharedLib.Service;

namespace GotorzApp.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
