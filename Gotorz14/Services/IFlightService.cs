using Gotorz14.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Gotorz14.Services
{
    public interface IFlightService
    {
        Task<List<Fligth>> GetAllFlights();
    }
}
