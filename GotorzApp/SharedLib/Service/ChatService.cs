using SharedLib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SharedLib.Service
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

        public async Task<bool> SaveMessageAsync(Chat newChat)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();
            if (newChat != null)
            {
                _context.Chats.Add(newChat);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        //public async Task<List<Chat>> GetUserChatsAsync(string userId)
        //{
        //    var _context = await _dbContextFactory.CreateDbContextAsync();

        //    return await _context.Chats
        //        .Where(c => c.SenderUserName == userId || c.ReceiverUserName == userId)
        //        .ToListAsync();
        //}

        //public async Task<List<Chat>> GetChatHistoryAsync(string userId1, string userId2)
        //{
        //    var _context = await _dbContextFactory.CreateDbContextAsync();
        //    return await _context.Chats
        //        .Where(c => (c.SenderUserName == userId1 && c.ReceiverUserName == userId2) ||
        //                    (c.SenderUserName == userId2 && c.ReceiverUserName == userId1))
        //        .ToListAsync();
        //}
    }
}
