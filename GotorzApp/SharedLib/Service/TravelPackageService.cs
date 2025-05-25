using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using SharedLib.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SharedLib.Service;

public class TravelPackageService : ITravelPackageService
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;
    private readonly IDistributedCache _cache;

    private const string CacheKey = "travelpackages_with_photos";

    public TravelPackageService(IDbContextFactory<GotorzContext> dbContextFactory, IDistributedCache cache)
    {
        _dbContextFactory = dbContextFactory;
        _cache = cache;
    }

    public async Task<List<TravelPackage>> GetAll()
    {
        var travelpackages = new List<TravelPackage>();
        try
        { 
            var cached = await _cache.GetStringAsync(CacheKey);
            if (cached != null)
            {
                return JsonSerializer.Deserialize<List<TravelPackage>>(cached, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
            }

            using var context = _dbContextFactory.CreateDbContext();
            travelpackages = await context.TravelPackages
                .Include(e => e.Flightpaths)
                    .ThenInclude(o => o.OutboundFlight.IataDestination)
                .Include(e => e.Flightpaths)
                    .ThenInclude(o => o.OutboundFlight.IataOrigin)
                .Include(e => e.Flightpaths)
                    .ThenInclude(h => h.HomeboundFlight.IataDestination)
                .Include(e => e.Flightpaths)
                    .ThenInclude(h => h.HomeboundFlight.IataOrigin)
                .Include(e => e.Hotel)
                .Include(e => e.Photo)
                .ToListAsync();

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            var json = JsonSerializer.Serialize(travelpackages, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = false
            });

            await _cache.SetStringAsync(CacheKey, json, options);
        
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while fetching travel packages: " + ex.Message);
            await _cache.RemoveAsync(CacheKey);

            using var context = _dbContextFactory.CreateDbContext();
            travelpackages = await context.TravelPackages
                .Include(e => e.Flightpaths)
                    .ThenInclude(o => o.OutboundFlight.IataDestination)
                .Include(e => e.Flightpaths)
                    .ThenInclude(o => o.OutboundFlight.IataOrigin)
                .Include(e => e.Flightpaths)
                    .ThenInclude(h => h.HomeboundFlight.IataDestination)
                .Include(e => e.Flightpaths)
                    .ThenInclude(h => h.HomeboundFlight.IataOrigin)
                .Include(e => e.Hotel)
                .Include(e => e.Photo)
                .ToListAsync();

            return travelpackages;

        }
        
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
        finally
        {
            // Clear the cache after adding a new travel package
            await _cache.RemoveAsync(CacheKey);
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
                .Include(e => e.Photo)
                .FirstOrDefault(tp => tp.Id == updatedTravelPackage.Id);

            if (existingTravelPackage == null)
            {
                return false;
            }
                        
            

            context.Entry(existingTravelPackage).CurrentValues.SetValues(updatedTravelPackage);

            if (existingTravelPackage.Hotel != updatedTravelPackage.Hotel)
                context.Entry(existingTravelPackage.Hotel).CurrentValues.SetValues(updatedTravelPackage.Hotel);

            if (updatedTravelPackage.Photo != null)
            {
                if (existingTravelPackage.Photo == null)
                {
                    existingTravelPackage.Photo = updatedTravelPackage.Photo;
                }
                else
                {
                    context.Entry(existingTravelPackage.Photo).CurrentValues.SetValues(updatedTravelPackage.Photo);
                }
            }
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
        finally
        {
            // Clear the cache after updating a travel package
            await _cache.RemoveAsync(CacheKey);
        }
    }

    public async Task<bool> Delete(TravelPackage travelPackage)
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
               .Include(e => e.Photo)
               .FirstOrDefault(tp => tp.Id == travelPackage.Id);

            if (existingTravelPackage == null)
            {
                return false;
            }

            context.Flightpaths.RemoveRange(existingTravelPackage.Flightpaths);
            context.Flights.RemoveRange(existingTravelPackage.Flightpaths.SelectMany(fp => new[] { fp.OutboundFlight, fp.HomeboundFlight }));
            context.TravelPackages.Remove(existingTravelPackage);
            context.Hotels.Remove(existingTravelPackage.Hotel);
            if (existingTravelPackage.Photo != null)
            {
                context.Photos.Remove(existingTravelPackage.Photo);
            }
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
        finally
        {
            // Clear the cache after deleting a travel package
            await _cache.RemoveAsync(CacheKey);
        }
    }
}
