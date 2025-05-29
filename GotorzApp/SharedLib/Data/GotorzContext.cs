using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SharedLib.Data;

public partial class GotorzContext
    : IdentityDbContext<GotorzAppUser, IdentityRole, string>
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
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Photo>(p =>
        {
            p.Property(e => e.PhotoName).IsRequired();
            p.Property(e => e.PhotoData).IsRequired();
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.Property(e => e.SentAt).HasColumnType("datetime");

            entity.HasOne(d => d.Sender).WithMany(p => p.SentChats)
                .HasForeignKey(d => d.SenderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1_Chats_Users");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ReceivedChats)
                .HasForeignKey(d => d.ReceiverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2_Chats_Users");
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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}