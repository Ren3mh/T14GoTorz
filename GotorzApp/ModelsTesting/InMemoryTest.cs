using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Data;
using Shared.Service;


namespace ModelsTesting
{
    public class InMemoryTest
    {
        [Fact]
        public void CanAddPackage()
        {
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

            var contextFactory = IDbContextFactory<GotorzContext, db>;
            var service = new TravelPackageService(db);
            service.Add(tp);
        }
    }
}
