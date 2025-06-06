﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLib.Data;

namespace SharedLib.Service;

public class IataLocationService : IIataLocationService
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

    public async Task<bool> Add(IataLocation x)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.IataLocations.Add(x);
        await context.SaveChangesAsync();
        return true;
    }

    public Task<IataLocation> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(IataLocation iata)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
