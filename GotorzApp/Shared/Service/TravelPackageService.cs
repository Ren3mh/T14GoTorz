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

    public async Task Add(TravelPackage newTravelPackage)
    {
        using var context = _dbContextFactory.CreateDbContext();
        using (var transaction = await context.Database.BeginTransactionAsync())
        try
        {
            // Add OutboundFlight
            context.Flights.Add(newTravelPackage.Flightpaths.First().OutboundFlight);
            context.SaveChanges();

            // Add HomeboundFlight
            context.Flights.Add(newTravelPackage.Flightpaths.First().HomeboundFlight);
            context.SaveChanges();

            // Add Flightpath
            context.Flightpaths.Add(newTravelPackage.Flightpaths.First());
            context.SaveChanges();

            // Commit the transaction if all operations are successful
            transaction.Commit();
        }
        catch (Exception)
        {
            // Rollback the transaction if any operation fails
            transaction.Rollback();
            throw;
        }
    }
}
