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

    public async Task SendMessage(string user, string message)
    {
        Console.WriteLine($"SendMessage called: {user}: {message}");

        //// Save the message to the database
        //var chat = new Chat
        //{
        //    SenderUserName = user,
        //    Message = message,
        //    SentAt = DateTime.UtcNow,
        //};

        // Broadcast to all clients
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
