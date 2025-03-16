using Gotorz14.Model;

namespace Gotorz14.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public Task<List<User>> GetAllUsers()
        {
            async _context.Users
        }
    }
}
