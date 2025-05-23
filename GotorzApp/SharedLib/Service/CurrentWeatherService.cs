using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace SharedLib.Service
{

    public class CurrentWeatherService : ICurrentWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CurrentWeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiKeys:OpenWeatherMapKey"];
        }

        public async Task<CurrentWeather>? GetCurrentWeather(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var currentWeatherResponse = JsonSerializer.Deserialize<CurrentWeather>(responseContent);

            return currentWeatherResponse;
        }
    }
}
