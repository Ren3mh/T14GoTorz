using Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Shared.Service
{
    public class ChatService
    {
        private readonly GotorzContext _context;

        public ChatService(GotorzContext context)
        {
            _context = context;
        }

        // Save a chat message
        public async Task SaveMessageAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        // Retrieve all messages for a user (sent or received)
        public async Task<List<Chat>> GetUserChatsAsync(string userId)
        {
            return await _context.Chats
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .OrderBy(c => c.SentAt)
                .ToListAsync();
        }
    }
}
