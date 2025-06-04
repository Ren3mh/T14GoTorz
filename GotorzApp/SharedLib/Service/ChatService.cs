using SharedLib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SharedLib.Service
{
    public class ChatService
    {
        private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

        public ChatService(IDbContextFactory<GotorzContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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

        public async Task<List<Chat>> GetUserChatsAsync(string userId)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();

            return await _context.Chats
                .Where(c => c.ReceiverUserId == userId)
                .ToListAsync();
        }
    }
}
