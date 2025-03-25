
namespace Gotorz14.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAll();

        //Task<T> Create();
    }
}
