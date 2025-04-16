using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;

public class FlightpathService : IService<Flightpath>
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

    public FlightpathService(IDbContextFactory<GotorzContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task<List<Flightpath>> GetAll()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var flightpaths = await context.Flightpaths
            .Include(e => e.HomeboundFlight)
            .Include(e => e.OutboundFlight)
            .ToListAsync();
        return flightpaths;
    }



    public async Task Add(Flightpath x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Flightpaths.Add(x);
        await context.SaveChangesAsync();
    }

}
