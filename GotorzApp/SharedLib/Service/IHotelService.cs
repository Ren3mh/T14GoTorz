using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Service
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetAll();
        Task<Hotel> GetById(int id);
        Task<bool> Add(Hotel hotel);
        Task<bool> Update(Hotel hotel);
        Task<bool> Delete(int id);
    }
}
