using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLib.Data;

namespace SharedLib.Service;

public class HotelService : IHotelService
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

    public HotelService(IDbContextFactory<GotorzContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Hotel>> GetAll()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var hotels = await context.Hotels
            .ToListAsync();
        return hotels;
    }

    public async Task<bool> Add(Hotel x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Hotels.Add(x);
        await context.SaveChangesAsync();
        return true;
    }

    public Task<Hotel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Hotel hotel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
