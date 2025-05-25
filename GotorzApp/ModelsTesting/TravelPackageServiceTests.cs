using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SharedLib;
using SharedLib.Data;
using SharedLib.Service;
using System;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ModelsTesting;

public class TravelPackageServiceTests
{
    private readonly IDistributedCache _redisCache;

    private readonly List<IataLocation> Iatas = new()
    {
        new IataLocation { Id = 1, City = "Copenhagen", Iata = "CPH" },
        new IataLocation { Id = 2, City = "London", Iata = "LON" },
        new IataLocation { Id = 3, City = "New York", Iata = "NYC" }
    };

    private DbContextOptions<GotorzContext> GetDbOptions(string databaseName)
    {
        return new DbContextOptionsBuilder<GotorzContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
    }

    private TravelPackageService CreateTravelPackageService(GotorzContext context)
    {
        var dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
        dbContextFactoryMock.Setup(factory => factory.CreateDbContext()).Returns(context);

        return new TravelPackageService(dbContextFactoryMock.Object, _redisCache);
    }

    public TravelPackageServiceTests()
    {
        _redisCache = new MemoryDistributedCache(
            Options.Create(new MemoryDistributedCacheOptions())
        );

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
                        IataOriginId = 2,
                        IataDestinationId = 3
                    },
                    HomeboundFlight = new Flight()
                    {
                        DepartureTime = DateTime.Now.AddDays(2),
                        ArrivalTime = DateTime.Now.AddDays(2).AddHours(2),
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
            },
            Photo = new Photo
            {
                PhotoName = "Test Photo",
                PhotoData = new byte[] { 1, 2, 3 }
            }
        };
    }

    [Fact]
    public async Task GetAll_ReturnsTravelPackages_AndCachesResult()
    {
        // Arrange
        await _redisCache.RemoveAsync("travelpackages_with_photos");

        var options = GetDbOptions("TestDb1");
        using var context = new GotorzContext(options);

        context.Database.EnsureCreated();
        context.IataLocations.AddRange(Iatas);
        context.SaveChanges();

        var travelPackageToInsert = CreateTravelPackage();
        var service = CreateTravelPackageService(context);

        var travelPackage = CreateTravelPackage();
        var photo = new Photo { PhotoName = "Test Photo", PhotoData = new byte[] { 1, 2, 3, 4 } };
        travelPackage.Photo = photo;
        service.Add(travelPackage).Wait();

        var newContext = new GotorzContext(options);
        var newService = CreateTravelPackageService(newContext);
        // Act (1): First call hits database
        var result1 = await newService.GetAll();
        Assert.Single(result1);

        // Act (2): Second call should hit cache
        var result2 = await newService.GetAll();
        Assert.Single(result2);

        // Optional: Direct Redis check
        var cachedJson = await _redisCache.GetStringAsync("travelpackages_with_photos");
        Assert.NotNull(cachedJson);

        

    }

    [Fact]
    public async Task Add_TravelPackage_Success()
    {
        // Arrange
        await _redisCache.RemoveAsync("travelpackages_with_photos");

        var options = GetDbOptions("TestDb2");
        using var context = new GotorzContext(options);

        context.Database.EnsureCreated();
        context.IataLocations.AddRange(Iatas);
        context.SaveChanges();

        var travelPackageToInsert = CreateTravelPackage();
        var service = CreateTravelPackageService(context);

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
        await _redisCache.RemoveAsync("travelpackages_with_photos");

        var options = GetDbOptions("TestDb3");
        using var context = new GotorzContext(options);

        context.Database.EnsureCreated();
        context.IataLocations.AddRange(Iatas);
        context.SaveChanges();

        var travelPackageToInsert = CreateTravelPackage();
        var service = CreateTravelPackageService(context);

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

        context.TravelPackages.AddRange(travelPackages);
        context.SaveChanges();

        travelPackages[0].Id = 1;
        travelPackages[1].Id = 2;

        // Act
        var result = await service.Delete(travelPackages[0]);

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

    [Fact]
    public async Task Update_TravelPackage_SuccessAsync()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GotorzContext>()
            .UseInMemoryDatabase(databaseName: "Test4")
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new GotorzContext(options);
        context.Database.EnsureCreated();

        // Seed the database with initial data
        var travelPackageToUpdate = CreateTravelPackage();
        travelPackageToUpdate.Id = 1;
        context.TravelPackages.Add(travelPackageToUpdate);
        context.SaveChanges();
        
        var dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
        dbContextFactoryMock.Setup(factory => factory.CreateDbContext()).Returns(context);
        var service = new TravelPackageService(dbContextFactoryMock.Object, _redisCache);
        
        // Act
        travelPackageToUpdate.Title = "Updated Title";
        var result = await service.Update(travelPackageToUpdate);
        
        // Assert
        Assert.True(result);

        var newContext = new GotorzContext(options);
        var updatedTravelPackage = await newContext.TravelPackages.FindAsync(1);
        Assert.NotNull(updatedTravelPackage);
        Assert.Equal("Updated Title", updatedTravelPackage.Title);
    }

}
