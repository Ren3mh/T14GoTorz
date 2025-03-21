using Gotorz14.Model;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class FlightService : IFlightService
    {

        private readonly FlightContext _context;
        
        public FlightService(FlightContext context)
        {
            _context = context;
        }
        public async Task<List<Fligth>> GetAllFlights()
        {
            var flights = await _context.Flights
                .ToListAsync();
            return flights;
        }
    }
}
