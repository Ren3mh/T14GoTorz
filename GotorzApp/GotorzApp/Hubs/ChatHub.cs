using Microsoft.AspNetCore.SignalR;
using SharedLib.Service;

namespace GotorzApp.Hubs;

public class ChatHub : Hub
{
    public async Task JoinPrivateChat(string customerId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, customerId);
    }

    public async Task SendMessageToGroup(string customerId, string fromUserId, string message)
    {
        await Clients.Group(customerId).SendAsync("ReceiveMessage", fromUserId, message);
    }
}
