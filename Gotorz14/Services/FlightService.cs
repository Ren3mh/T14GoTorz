//using Gotorz14.Data;
using Gotorz14.Models1;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class FlightService : IService<Gotorz14.Models1.Flight>
    {

        private readonly GotorzContext _context;

        public FlightService(GotorzContext context)
        {
            _context = context;
        }

        public async Task Create(Gotorz14.Models1.Flight x)
        {
            _context.Flights.Add(x);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Gotorz14.Models1.Flight>> GetAll()
        {
            var flights = await _context.Flights
                .ToListAsync();
            return flights;
        }
    }
}
