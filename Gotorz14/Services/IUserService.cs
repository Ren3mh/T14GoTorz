using Gotorz14.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Gotorz14.Services
{
    public interface IUserService
    {
        Task<List<FlightPath>> GetAllUsers();
    }
}
