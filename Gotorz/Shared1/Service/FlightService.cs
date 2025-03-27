using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service
{
    public class FlightService : IFlightService
    {

        private readonly AppDbContext _context;

        public FlightService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Flight>> GetAllFlights()
        {
            var flights = await _context.Flights
                .Include(fp => fp.Outbound)
                .Include(fp => fp.Homebound)
                .ToListAsync();
            return flights;
        }
    }

}
