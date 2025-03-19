using System.Reflection.Emit;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlightPath>()
                .HasOne(fp => fp.Outbound)
                .WithMany()
                .HasForeignKey(fp => fp.OutboundId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<FlightPath>()
                .HasOne(fp => fp.Homebound)
                .WithMany()
                .HasForeignKey(fp => fp.HomeboundId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction
        }

        public DbSet<User> UsersTable { get; set; }

        public DbSet<FlightPath> FlightPathsTable { get; set; }
        public DbSet<Fligth> FlightsTable { get; set; }


    }
}
