using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;

public class HotelService : IService<Hotel>
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

    public async Task Add(Hotel x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Hotels.Add(x);
        await context.SaveChangesAsync();
    }
}
