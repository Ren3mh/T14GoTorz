using Gotorz14.Data;
using Gotorz14.Models;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class FlightpathService : IService<Flightpath>
    {
        readonly GotorzContext _context;

        public FlightpathService(GotorzContext context) 
        {
            _context = context;
        }
        public async Task<List<Flightpath>> GetAll()
        {
            var flightpaths = await _context.Flightpaths
                .Include(e => e.Flights)
                .ToListAsync();

            return flightpaths;
        }
    }
}
