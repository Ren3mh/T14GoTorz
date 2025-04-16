using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;
public class FlightService : IService<Flight>
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

    public FlightService(IDbContextFactory<GotorzContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task Add(Flight x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Add(x);
        await context.SaveChangesAsync();
    }

    public async Task<List<Flight>> GetAll()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var flights = await context.Flights
        .Include(e => e.Iataorigin)
        .Include(e => e.Iatadestination)
        .ToListAsync();
        return flights;
    }
}

