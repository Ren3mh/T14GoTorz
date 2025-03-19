using Gotorz14.Model;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<FlightPath>> GetAllUsers()
        {
            var users = await _context.FlightPathsTable
                .Include(fp => fp.Outbound)
                .Include(fp => fp.Homebound)
                .ToListAsync();
            return users;
        }
    }
}
