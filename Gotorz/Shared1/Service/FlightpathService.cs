using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service
{
    public class FlightpathService : IService<Flightpath>
    {

        private readonly GotorzContext _context;

        public FlightpathService(GotorzContext context)
        {
            _context = context;
        }
        public async Task<List<Flightpath>> GetAll()
        {
            var flightpaths = await _context.Flightpaths
                .Include(e => e.HomeboundFlight)
                .Include(e => e.OutboundFlight)
                .ToListAsync();
            return flightpaths;
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
