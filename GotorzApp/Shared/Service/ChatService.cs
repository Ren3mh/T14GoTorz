using Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace Shared.Service
{
    public class ChatService
    {
        private readonly IDbContextFactory<GotorzContext> _dbContextFactory;
        private readonly UserManager<GotorzAppUser> _userManager;

        public ChatService(IDbContextFactory<GotorzContext> dbContextFactory, UserManager<GotorzAppUser> userManager)
        {
            _dbContextFactory = dbContextFactory;
            _userManager = userManager;
        }

        // Save a chat message
        public async Task SaveMessageAsync(Chat newChat, ClaimsPrincipal identity)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();
            var user = await _userManager.GetUserAsync(identity);
            if (user != null)
            {
                newChat.UserId = user.Id; // Set the UserId property 

                _context.Chats.Add(newChat);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve all messages for a user (sent or received)
        public async Task<List<Chat>> GetUserChatsAsync()
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();

            return await _context.Chats
                .ToListAsync();
        }

        // Retrieve all messages between two users
        public async Task<List<Chat>> GetChatHistoryAsync(string userId1, string userId2)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();
            return await _context.Chats
                .Where(c => (c.SenderUserName == userId1 && c.ReceiverUserName == userId2) ||
                            (c.SenderUserName == userId2 && c.ReceiverUserName == userId1))
                .ToListAsync();
        }

        //// Retrieve all usernames and ids a user has chatted with
        //public async Task<List<string>> GetChatUsernamesAsync(string userId)
        //{
        //    var _context = await _dbContextFactory.CreateDbContextAsync();
        //    return await _context.Chats
        //        .Where(c => c.SenderUserName == userId || c.ReceiverUserName == userId)
        //        .Select(c => c.SenderUserName == userId ? c.ReceiverUserName : c.SenderUserName)
        //        .Distinct()
        //        .ToListAsync();
        //}

    }
}
