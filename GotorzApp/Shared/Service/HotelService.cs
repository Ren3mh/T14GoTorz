using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Shared.Service
{
    public class HotelService : IService<Hotel>
    {

        private readonly GotorzContext _context;

        public HotelService(GotorzContext context)
        {
            _context = context;
        }

        public async Task<List<Hotel>> GetAll()
        {
            var hotels = await _context.Hotels
                .ToListAsync();
            return hotels;
        }

        //private readonly GotorzContext _context;

        //public FlightService(GotorzContext context)
        //{
        //    _context = context;
        //}

        public async Task Add(Hotel x)
        {
            _context.Hotels.Add(x);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<Gotorz14.Models1.Flight>> GetAll()
        //{
        //    var flights = await _context.Flights
        //        .ToListAsync();
        //    return flights;
        //}

    }

}
