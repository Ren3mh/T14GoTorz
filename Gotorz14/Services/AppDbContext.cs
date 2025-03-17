using Gotorz14.Model;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


    }
}
