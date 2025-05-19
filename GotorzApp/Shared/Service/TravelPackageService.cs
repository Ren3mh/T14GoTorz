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
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var tp = context.TravelPackages.Add(newTravelPackage);
            context.SaveChanges();

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            // Rollback the transaction if any operation fails
            transaction.Rollback();
            Console.WriteLine("Error occurred while adding travel package: " + newTravelPackage.Title);
            Console.WriteLine($"Exception: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> Update(TravelPackage updatedTravelPackage)
    {
        using var context = _dbContextFactory.CreateDbContext();
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var existingTravelPackage = context.TravelPackages
                .Include(e => e.Flightpaths)
                .ThenInclude(o => o.OutboundFlight.IataDestination)
                .Include(e => e.Flightpaths)
                .ThenInclude(o => o.OutboundFlight.IataOrigin)
                .Include(e => e.Flightpaths)
                .ThenInclude(h => h.HomeboundFlight.IataDestination)
                .Include(e => e.Flightpaths)
                .ThenInclude(h => h.HomeboundFlight.IataOrigin)
                .Include(e => e.Hotel)
                .FirstOrDefault(tp => tp.Id == updatedTravelPackage.Id);

            if (existingTravelPackage == null)
            {
                return false;
            }
            context.Entry(existingTravelPackage).CurrentValues.SetValues(updatedTravelPackage);
            context.Entry(existingTravelPackage.Hotel).CurrentValues.SetValues(updatedTravelPackage.Hotel);
            context.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            // Rollback the transaction if any operation fails
            transaction.Rollback();
            Console.WriteLine("Error occurred while updating travel package: " + updatedTravelPackage.Title);
            Console.WriteLine($"Exception: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> Delete(TravelPackage travelPackage)
    {
        using var context = _dbContextFactory.CreateDbContext();
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            if (travelPackage == null)
            {
                return false;
            }

            context.Flightpaths.RemoveRange(travelPackage.Flightpaths);
            context.Flights.RemoveRange(travelPackage.Flightpaths.SelectMany(fp => new[] { fp.OutboundFlight, fp.HomeboundFlight }));
            context.TravelPackages.Remove(travelPackage);
            context.Hotels.Remove(travelPackage.Hotel);
            context.SaveChanges();

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            // Rollback the transaction if any operation fails
            transaction.Rollback();
            Console.WriteLine("Error occurred while deleting travel package");
            Console.WriteLine($"Exception: {ex.Message}");
            return false;
        }
    }
}
