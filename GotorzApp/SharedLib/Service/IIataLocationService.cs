using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Service
{
    public interface IIataLocationService
    {
        Task<List<IataLocation>> GetAll();
        Task<bool> Add(IataLocation iata);
        Task<IataLocation> GetById(int id);
        Task<bool> Update(IataLocation iata);
        Task<bool> Delete(int id);
    }
}
