using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;
public class FlightService : IFlightService
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

    public FlightService(IDbContextFactory<GotorzContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Flight>> GetAll()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var flights = await context.Flights
        .Include(e => e.IataOrigin)
        .Include(e => e.IataDestination)
        .ToListAsync();

        return flights;
    }

    public Task<Flight> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Add(Flight flight)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Add(flight);
        await context.SaveChangesAsync();
        return true;
    }
    public Task<bool> Update(Flight flight)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }   
}

