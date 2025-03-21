using System.Reflection.Emit;
using Gotorz14.Model;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class FlightPathContext : DbContext
    {
        public FlightPathContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure the FlightPath entity
            modelBuilder.Entity<Flightpath>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Fare)
                        .IsRequired()
                        .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Luggage)
                        .IsRequired();

                entity.HasOne(e => e.Outbound)
                        .WithMany()
                        .HasForeignKey("OutboundId")
                        .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Homebound)
                        .WithMany()
                        .HasForeignKey("HomeboundId")
                        .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Flightpath> FlightPathsTable { get; set; }
    }
}
