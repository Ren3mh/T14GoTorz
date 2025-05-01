using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAll();
        Task<Flight> GetById(int id);
        Task<bool> Add(Flight flight);
        Task<bool> Update(Flight flight);
        Task<bool> Delete(int id);
    }
}
