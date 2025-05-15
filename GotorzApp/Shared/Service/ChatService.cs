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
        public async Task SaveMessageAsync(Chat newChat, IIdentity identity)
        {
            var claims = new ClaimsPrincipal(identity);
            var _context = await _dbContextFactory.CreateDbContextAsync();
            var user = await _userManager.GetUserAsync(claims);
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
    }
}
