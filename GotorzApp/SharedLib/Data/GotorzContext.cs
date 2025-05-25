using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // ✅

namespace SharedLib.Data;

public partial class GotorzContext : IdentityDbContext<GotorzAppUser>
{
    public GotorzContext(DbContextOptions<GotorzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Flightpath> Flightpaths { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<IataLocation> IataLocations { get; set; }

    public virtual DbSet<TravelPackage> TravelPackages { get; set; }

    public virtual DbSet<VwFlight> VwFlights { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Photo>(p =>
        {
            p.HasKey(e => e.Id).HasName("PK_Photos");
            p.Property(e => e.Id).ValueGeneratedOnAdd();
            p.Property(e => e.PhotoName).IsRequired();
            p.Property(e => e.PhotoData).IsRequired();
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.Property(e => e.Message).IsRequired();
            entity.Property(e => e.SenderUserName).IsRequired();
            entity.Property(e => e.SentAt).HasColumnType("datetime");

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);
        });


        modelBuilder.Entity<Flight>(entity =>
        {

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.IataDestinationId).HasColumnName("IATADestinationId");
            entity.Property(e => e.IataOriginId).HasColumnName("IATAOriginId");

            entity.HasOne(d => d.IataDestination).WithMany(p => p.FlightIatadestinations)
                .HasForeignKey(d => d.IataDestinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1_Flights_IATA_locations");

            entity.HasOne(d => d.IataOrigin).WithMany(p => p.FlightIataorigins)
                .HasForeignKey(d => d.IataOriginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2_Flights_IATA_locations");
        });

        modelBuilder.Entity<Flightpath>(entity =>
        {
            entity.Property(e => e.Fare).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.HomeboundFlight).WithMany(p => p.FlightpathHomeboundFlights)
                .HasForeignKey(d => d.HomeboundFlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK3_Flightpaths_Flights");

            entity.HasOne(d => d.OutboundFlight).WithMany(p => p.FlightpathOutboundFlights)
                .HasForeignKey(d => d.OutboundFlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2_Flightpaths_Flights");

            entity.HasOne(d => d.TravelPackage).WithMany(p => p.Flightpaths)
                .HasForeignKey(d => d.TravelPackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1_Flightpaths_TravelPackages");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.CheckIn).HasColumnType("datetime");
            entity.Property(e => e.CheckOut).HasColumnType("datetime");
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Name).IsRequired().HasColumnName("HotelName");
            entity.Property(e => e.Rate).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Telephonenumber).IsRequired();
        });

        modelBuilder.Entity<IataLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IATA_locations");

            entity.ToTable("IATA_locations");

            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.Iata)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("IATA");
        });

        modelBuilder.Entity<TravelPackage>(entity =>
        {
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.Hotel).WithMany(p => p.TravelPackages)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1_TravelPackages_Hotels");

            entity.HasOne(d => d.Photo).WithMany(p => p.TravelPackages)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2_TravelPackages_Photos");
        });

        modelBuilder.Entity<VwFlight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwFlights");

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.Destination).IsRequired();
            entity.Property(e => e.IataDestination)
                .IsRequired()
                .HasMaxLength(3);
            entity.Property(e => e.IataOrigin)
                .IsRequired()
                .HasMaxLength(3);
            entity.Property(e => e.Origin).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}