using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared;
using Shared.Data;
using Shared.Service;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;


namespace ModelsTesting
{
    public class InMemoryTest
    {
        private readonly Mock<ITravelPackageService> travelPackageServiceMock;

        public InMemoryTest()
        {
            mockDbFactory = new Mock<IDbContextFactory<GotorzContext>>();
        }

        [Fact]
        public void CanAddPackage()
        {
            mockDbFactory.Setup(f => f.CreateDbContext())
            .Returns(new SomeDbContext(new DbContextOptionsBuilder<SomeDbContext>()
            .UseInMemoryDatabase("InMemoryTest")
            .Options));

            var tp = new TravelPackage()
            {
                Id = 1,
                Title = "Test Package",
                Description = "Test Description",
                Flightpaths = new List<Flightpath>
                {
                    new Flightpath
                    {
                        Id = 1,
                        Fare = 1000,
                        OutboundFlightId = 1,
                        HomeboundFlightId = 2
                    }
                },
                Hotel = new Hotel
                {
                    Id = 1,
                    Name = "Test Hotel",
                    Address = "Test Address"
                }
            };

            var db = GotorzInMemoryContext.GetMemoryContext();

            var service = new TravelPackageService(new DbContextFactory<GotorzContext>());

            //new TravelPackageService(DbContextFactory<GotorzContext>(options =>
            //options.UseInMemoryDatabase(databaseName: "InMemoryDatabase")));
            service.Add(tp);
        }
    }
}
