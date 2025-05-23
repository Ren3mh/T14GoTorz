
namespace SharedLib.Service;

public interface ITravelPackageService
{
    Task<List<TravelPackage>> GetAll();
    Task<TravelPackage> GetById(int id);
    Task<bool> Add(TravelPackage travelPackage);
    Task<bool> Update(TravelPackage TravelPackage);
    Task<bool> Delete(TravelPackage travelPackage);
}