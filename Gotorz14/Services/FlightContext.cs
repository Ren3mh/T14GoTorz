using Gotorz14.Model;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fligth>(entity =>
            {
                entity
                    .HasKey(e => e.Id);
                
                entity
                    .ToView("Flights");

                entity.Property(e => e.IataDestination).HasMaxLength(3);
                entity.Property(e => e.IataOrigin).HasMaxLength(3);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Fligth> Flights { get; set; }
    }
}
