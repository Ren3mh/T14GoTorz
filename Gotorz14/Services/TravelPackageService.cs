using Gotorz14.Data;
using Gotorz14.Models;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14.Services
{
    public class TravelPackageService : IService<TravelPackage>
    {
        readonly GotorzContext _context;

        public TravelPackageService(GotorzContext context) 
        {
            _context = context;
        }
        public async Task<List<TravelPackage>> GetAll()
        {
            var travelPackages = await _context.TravelPackages
                .Include(e => e.Hotel)
                .Include(e => e.Flightpaths)
                .ToListAsync();

            return travelPackages;
        }
    }
}
