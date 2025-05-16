using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Shared;
using Shared.Data;
using Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsTesting
{
    public class TravelPackageServiceTests
    {
        private readonly Mock<IDbContextFactory<GotorzContext>> _dbContextFactoryMock;
        private readonly TravelPackageService _service;

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

            using var context = new GotorzContext(options);
            context.Database.EnsureCreated();

            // Seed the database with initial data
            var Iatas = new List<IataLocation>()
            {
                new IataLocation { Id = 1, Iata = "TST", City = "Not A City" },
                new IataLocation { Id = 2, Iata = "JFK", City = "New York" },
                new IataLocation { Id = 3, Iata = "LAX", City = "Los Angeles" },
            };

            context.IataLocations.AddRange(Iatas);
            context.SaveChanges();

            var travelPackageToInsert = new TravelPackage
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
                }
            };

            var dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
            dbContextFactoryMock.Setup(factory => factory.CreateDbContext()).Returns(context);

            var service = new TravelPackageService(dbContextFactoryMock.Object);

            // Act
            var result = await service.Add(travelPackageToInsert);

            // Assert
            Assert.True(result);

            var insertedPackage = await context.TravelPackages
                .Include(tp => tp.Flightpaths)
                    .ThenInclude(fp => fp.OutboundFlight)
                .Include(tp => tp.Flightpaths)
                    .ThenInclude(fp => fp.HomeboundFlight)
                .FirstOrDefaultAsync(tp => tp.Id == 1);

            Assert.NotNull(insertedPackage);
            Assert.Equal("Test Package", insertedPackage.Title);
            Assert.Equal("Test Description", insertedPackage.Description);
            Assert.Single(insertedPackage.Flightpaths);

            var flightpath = insertedPackage.Flightpaths.First();
            Assert.NotNull(flightpath.OutboundFlight);
            Assert.NotNull(flightpath.HomeboundFlight);

            Assert.Equal("JFK", flightpath.OutboundFlight.IataOrigin.Iata);
            Assert.Equal("LAX", flightpath.OutboundFlight.IataDestination.Iata);
            Assert.Equal("LAX", flightpath.HomeboundFlight.IataOrigin.Iata);
            Assert.Equal("JFK", flightpath.HomeboundFlight.IataDestination.Iata);
        }
    }
}
