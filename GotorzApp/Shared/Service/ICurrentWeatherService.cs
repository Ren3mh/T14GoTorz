
namespace Shared.Service;

public interface ICurrentWeatherService
{
    Task<CurrentWeather>? GetCurrentWeather(string city);
}
