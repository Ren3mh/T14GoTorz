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
            // Configure the Flight entity
            modelBuilder.Entity<Fligth>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DepartureTime)
                        .IsRequired();

                entity.Property(e => e.ArrivalTime)
                        .IsRequired();

                entity.Property(e => e.Destination)
                        .IsRequired()
                        .HasMaxLength(100);

                entity.Property(e => e.Origin)
                        .IsRequired()
                        .HasMaxLength(100);

                entity.Property(e => e.IataDestination)
                        .IsRequired()
                        .HasMaxLength(3);

                entity.Property(e => e.IataOrigin)
                        .IsRequired()
                        .HasMaxLength(3);
            });

            // Configure the FlightPath entity
            modelBuilder.Entity<FlightPath>(entity =>
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
        

        public DbSet<User> UsersTable { get; set; }

        public DbSet<FlightPath> FlightPathsTable { get; set; }
        public DbSet<Fligth> FlightsTable { get; set; }

    }
}
