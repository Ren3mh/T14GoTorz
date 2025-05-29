using Microsoft.AspNetCore.SignalR;
using SharedLib.Service;
using SharedLib;

namespace GotorzApp.Hubs;

public class ChatHub : Hub
{

    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task JoinPrivateChat(string customerId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, customerId);
    }

    public async Task SendMessageToGroup(string customerId, string fromUserId, string message)
    {
        if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(fromUserId) || string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Customer ID, from user ID, and message cannot be null or empty.");
        }

        var sentChat = new Chat(message, fromUserId, customerId, DateTime.UtcNow);

        var success = await _chatService.SaveMessageAsync(sentChat);

        await Clients.Group(customerId).SendAsync("ReceiveMessage", fromUserId, message);
    }
}
