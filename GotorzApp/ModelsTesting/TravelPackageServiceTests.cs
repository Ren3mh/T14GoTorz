using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Shared;
using Shared.Data;
using Shared.Service;

namespace ModelsTesting;

public class TravelPackageServiceTests
{
    private readonly Mock<IDbContextFactory<GotorzContext>> _dbContextFactoryMock;
    private readonly TravelPackageService _service;
    private readonly List<IataLocation> Iatas = new()
    {
        new IataLocation { Id = 1, City = "Copenhagen", Iata = "CPH" },
        new IataLocation { Id = 2, City = "London", Iata = "LON" },
        new IataLocation { Id = 3, City = "New York", Iata = "NYC" }
    };

    public TravelPackageServiceTests()
    {
        _dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
        _service = new TravelPackageService(_dbContextFactoryMock.Object);
    }

    [Fact]
    public async Task Add_TravelPackage_Success()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GotorzContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new GotorzContext(options);
        context.Database.EnsureCreated();

        context.IataLocations.AddRange(Iatas);
        context.SaveChanges();

        var travelPackageToInsert = CreateTravelPackage();

        var dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
        dbContextFactoryMock.Setup(factory => factory.CreateDbContext()).Returns(context);

        var service = new TravelPackageService(dbContextFactoryMock.Object);

        // Act
        var result = await service.Add(travelPackageToInsert);

        // Assert
        Assert.True(result);

        var newContext = new GotorzContext(options);
        var insertedTravelPackage = await newContext.TravelPackages.FindAsync(1);
        Assert.NotNull(insertedTravelPackage);

        Assert.True(newContext.Hotels.Count() == 1);
        Assert.True(newContext.Flightpaths.Count() == 1);
        Assert.True(newContext.Flights.Count() == 2);
        Assert.True(newContext.IataLocations.Count() == 3);
    }

    [Fact]
    public async Task Delete_TravelPackage_SuccessAsync()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GotorzContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new GotorzContext(options);
        context.Database.EnsureCreated();

        // Seed the database with initial data
        var travelPackages = new List<TravelPackage>() 
        { 
            CreateTravelPackage(),
            CreateTravelPackage()
        };
        travelPackages[0].Flightpaths.Add(new Flightpath()
        {
            HomeboundFlight = new Flight()
            {
                ArrivalTime = DateTime.Now.AddDays(2).AddHours(2),
                DepartureTime = DateTime.Now.AddDays(2),
                IataOriginId = 2,
                IataDestinationId = 3
            },
            OutboundFlight = new Flight()
            {
                ArrivalTime = DateTime.Now.AddDays(1).AddHours(2),
                DepartureTime = DateTime.Now.AddDays(1),
                IataOriginId = 3,
                IataDestinationId = 2
            }
        });

        context.IataLocations.AddRange(Iatas);
        context.SaveChanges();
        context.TravelPackages.AddRange(travelPackages);
        context.SaveChanges();

        var dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
        dbContextFactoryMock.Setup(factory => factory.CreateDbContext()).Returns(context);
        var service = new TravelPackageService(dbContextFactoryMock.Object);

        // Act
        var result = await service.Delete(1);

        // Assert
        Assert.True(result);

        var newContext = new GotorzContext(options);
        var deletedTravelPackage = await newContext.TravelPackages.FindAsync(1);
        Assert.Null(deletedTravelPackage);
        var notDeletedTravelPackage = await newContext.TravelPackages.FindAsync(2);
        Assert.NotNull(notDeletedTravelPackage);

        Assert.True(newContext.Hotels.Count() == 1);
        Assert.True(newContext.Flightpaths.Count() == 1);
        Assert.True(newContext.Flights.Count() == 2);
        Assert.True(newContext.IataLocations.Count() == 3);
    }

    private TravelPackage CreateTravelPackage()
    {
        return new TravelPackage()
        {
            Title = "Test Package",
            Description = "Test Description",
            Flightpaths = new List<Flightpath>
            {
                new Flightpath
                {
                    Fare = 1000,
                    Luggage = true,
                    OutboundFlight = new Flight()
                    {
                        DepartureTime = DateTime.Now.AddDays(1),
                        ArrivalTime = DateTime.Now.AddDays(1).AddHours(2),
                        //IataOrigin = Iatas[1],
                        //IataDestination = Iatas[2],
                        IataOriginId = 2,
                        IataDestinationId = 3
                    },
                    HomeboundFlight = new Flight()
                    {
                        DepartureTime = DateTime.Now.AddDays(2),
                        ArrivalTime = DateTime.Now.AddDays(2).AddHours(2),
                        //IataOrigin = Iatas[2],
                        //IataDestination = Iatas[1],
                        IataDestinationId = 2,
                        IataOriginId = 3
                    },
                },
            },
            Hotel = new Hotel()
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Name = "Test Hotel",
                Address = "123 Test St",
                Telephonenumber = "1234567890",
                Email = "test@email.com",
                Rate = 200,
                Description = "Test Hotel Description"
            }
        };
    }
}
