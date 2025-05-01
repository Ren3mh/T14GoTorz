using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;

public class TravelPackageService : ITravelPackageService
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
            .ThenInclude(o => o.OutboundFlight.IataDestination)
            .Include(e => e.Flightpaths)
            .ThenInclude(o => o.OutboundFlight.IataOrigin)

            .Include(e => e.Flightpaths)
            .ThenInclude(h => h.HomeboundFlight.IataDestination)
            .Include(e => e.Flightpaths)
            .ThenInclude(h => h.HomeboundFlight.IataOrigin)

            .Include(e => e.Hotel)
            .ToListAsync();
        return travelpackages;
    }

    public Task<TravelPackage> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Add(TravelPackage newTravelPackage)
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
            return true;
        }
        catch (Exception)
        {
            // Rollback the transaction if any operation fails
            transaction.Rollback();
            throw;
        }
    }

    public Task<bool> Update(TravelPackage TravelPackage)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
