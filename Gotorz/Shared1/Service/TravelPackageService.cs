using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service
{
    public class TravelPackageService : IService<TravelPackage>
    {

        private readonly GotorzContext _context;

        public TravelPackageService(GotorzContext context)
        {
            _context = context;
        }
        public async Task<List<TravelPackage>> GetAll()
        {
            var travelpackages = await _context.TravelPackages
                .Include(e => e.Flightpaths)
                .ThenInclude(o => o.OutboundFlight.Iatadestination)
                .Include(e => e.Flightpaths)
                .ThenInclude(o => o.OutboundFlight.Iataorigin)

                .Include(e => e.Flightpaths)
                .ThenInclude(h => h.HomeboundFlight.Iatadestination)
                .Include(e => e.Flightpaths)
                .ThenInclude(h => h.HomeboundFlight.Iataorigin)

                .Include(e => e.Hotel)
                .ToListAsync();
            return travelpackages;
        }

        //private readonly GotorzContext _context;

        //public FlightService(GotorzContext context)
        //{
        //    _context = context;
        //}

        //public async Task Create(Gotorz14.Models1.Flight x)
        //{
        //    _context.Flights.Add(x);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<List<Gotorz14.Models1.Flight>> GetAll()
        //{
        //    var flights = await _context.Flights
        //        .ToListAsync();
        //    return flights;
        //}

    }

}
