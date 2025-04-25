using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shared;
using Shared.Service;
using Xunit;
using Shared.Data;


namespace ModelsTesting
{
    public class FlightServiceTests
    {
        private readonly Mock<IDbContextFactory<GotorzContext>> _dbContextFactoryMock;
        private readonly Mock<GotorzContext> _dbContextMock;
        private readonly Mock<DbSet<Flight>> _dbSetMock;
        private readonly FlightService _flightService;

        public FlightServiceTests()
        {
            _dbContextFactoryMock = new Mock<IDbContextFactory<GotorzContext>>();
            _dbContextMock = new Mock<GotorzContext>(new DbContextOptions<GotorzContext>());
            _dbSetMock = new Mock<DbSet<Flight>>();

            _dbContextFactoryMock.Setup(factory => factory.CreateDbContext()).Returns(_dbContextMock.Object);
            _dbContextMock.Setup(context => context.Flights).Returns(_dbSetMock.Object);

            _flightService = new FlightService(_dbContextFactoryMock.Object);
        }

        [Fact]
        public async Task Add_Should_Add_Flight_To_Database()
        {
            // Arrange
            var flight = new Flight
            {
                Id = 1,
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2),
                IataOrigin = new IataLocation { Id = 1, Iata = "JFK", City = "New York" },
                IataDestination = new IataLocation { Id = 2, Iata = "LAX", City = "Los Angeles" }
            };

            _dbSetMock.Setup(set => set.Add(It.IsAny<Flight>())).Verifiable();
            _dbContextMock.Setup(context => context.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            await _flightService.Add(flight);

            // Assert
            _dbSetMock.Verify(set => set.Add(It.Is<Flight>(f => f == flight)), Times.Once);
            _dbContextMock.Verify(context => context.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetAll_Should_Return_All_Flights()
        {
            // Arrange
            var flights = new List<Flight>
        {
            new Flight
            {
                Id = 1,
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2),
                IataOrigin = new IataLocation { Id = 1, Iata = "JFK", City = "New York" },
                IataDestination = new IataLocation { Id = 2, Iata = "LAX", City = "Los Angeles" }
            },
            new Flight
            {
                Id = 2,
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(3),
                IataOrigin = new IataLocation { Id = 3, Iata = "ORD", City = "Chicago" },
                IataDestination = new IataLocation { Id = 4, Iata = "MIA", City = "Miami" }
            }
        }.AsQueryable();

            _dbSetMock.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(flights.Provider);
            _dbSetMock.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(flights.Expression);
            _dbSetMock.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(flights.ElementType);
            _dbSetMock.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(flights.GetEnumerator());

            _dbContextMock.Setup(context => context.Flights).Returns(_dbSetMock.Object);

            // Act
            var result = await _flightService.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("New York", result[0].IataOrigin.City);
            Assert.Equal("Los Angeles", result[0].IataDestination.City);
            Assert.Equal("Chicago", result[1].IataOrigin.City);
            Assert.Equal("Miami", result[1].IataDestination.City);
        }
    }
}
