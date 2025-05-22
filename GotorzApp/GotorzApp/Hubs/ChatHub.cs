using Microsoft.AspNetCore.SignalR;
using Shared.Service;
using Shared;
using System.Security.Claims;

namespace GotorzApp.Hubs;

public class ChatHub : Hub
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(string senderUserName, string receiverUserName, string message)
    {
        // Send to both sender and receiver
        await Clients.Users(senderUserName, receiverUserName)
            .SendAsync("ReceiveMessage", senderUserName, receiverUserName, message);
    }
}
