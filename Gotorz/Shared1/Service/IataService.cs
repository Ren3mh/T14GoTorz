using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service
{
    public class IataLocationService: IService<IataLocation>
    {

        private readonly GotorzContext _context;

        public IataLocationService(GotorzContext context)
        {
            _context = context;
        }
        public async Task<List<IataLocation>> GetAll()
        {
            var iatas = await _context.IataLocations
                .ToListAsync();
            return iatas;
        }

        public async Task Add(IataLocation x)
        {
            _context.IataLocations.Add(x);
            await _context.SaveChangesAsync();
        }


    }

}
