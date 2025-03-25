using Gotorz14.Data;
using Gotorz14.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class FlightService : IService<Flight>
    {

        private readonly GotorzContext _context;

        public FlightService(GotorzContext context)
        {
            _context = context;
        }


        public async Task<List<Flight>> GetAll()
        {
            var flights = await _context.Flights
                .ToListAsync();
            return flights;
        }
    }
}
