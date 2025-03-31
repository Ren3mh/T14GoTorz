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



        public async Task Add(Flightpath x)
        {
            _context.Flightpaths.Add(x);
            await _context.SaveChangesAsync();
        }

    }

}
