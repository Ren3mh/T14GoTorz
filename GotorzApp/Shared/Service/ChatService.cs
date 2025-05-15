using Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Shared.Service
{
    public class ChatService
    {
        private readonly IDbContextFactory<GotorzContext> _dbContextFactory;


        public ChatService(IDbContextFactory<GotorzContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // Save a chat message
        public async Task SaveMessageAsync(Chat chat)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        // Retrieve all messages for a user (sent or received)
        public async Task<List<Chat>> GetUserChatsAsync()
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();

            return await _context.Chats
                .ToListAsync();
        }
    }
}
