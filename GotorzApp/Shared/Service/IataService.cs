using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service;

public class IataLocationService: IService<IataLocation>
{

    private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

    public IataLocationService(IDbContextFactory<GotorzContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<IataLocation>> GetAll()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var iatas = await context.IataLocations
            .ToListAsync();
        return iatas;
    }

    public async Task Add(IataLocation x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.IataLocations.Add(x);
        await context.SaveChangesAsync();
    }
}
