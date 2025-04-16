using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;

public class TravelPackageService : IService<TravelPackage>
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;


    public TravelPackageService(IDbContextFactory<GotorzContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<TravelPackage>> GetAll()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var travelpackages = await context.TravelPackages
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

    public async Task Add(TravelPackage x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.TravelPackages.Add(x);
        await context.SaveChangesAsync();
    }
}
