
namespace SharedLib.Service;

public interface IPhotoService
{
    Task<List<Photo>> GetAll();
    Task<Photo> GetById(int id);
    Task<bool> Add(Photo photo);
    Task<bool> Update(Photo photo);
    Task<bool> Delete(int id);
}