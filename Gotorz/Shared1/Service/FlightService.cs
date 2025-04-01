using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Data;

namespace Shared.Service
{
    public class FlightService : IService<Flight>
    {
        private readonly GotorzContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public FlightService(GotorzContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _clientId = configuration["AmadeusApi:ClientId"];
            _clientSecret = configuration["AmadeusApi:ClientSecret"];
        }

        public async Task Add(Flight x)
        {
            _context.Add(x);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Flight>> GetAll()
        {
            var flights = await _context.Flights
                .Include(e => e.Iataorigin)
                .Include(e => e.Iatadestination)
                .ToListAsync();
            return flights;
        }

        public async Task<string> GetAccessToken()
        {
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            });

            var response = await _httpClient.PostAsync("https://test.api.amadeus.com/v1/security/oauth2/token", requestContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

            return tokenResponse.AccessToken;
        }

        public async Task<List<FlightDestination>> GetFlightDestinations(string origin, decimal maxPrice)
        {
            var token = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"https://test.api.amadeus.com/v1/shopping/flight-destinations?origin={origin}&maxPrice={maxPrice}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var flightResponse = JsonSerializer.Deserialize<FlightDestinationsResponse>(responseContent);

            return flightResponse.Data;
        }
    }

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }

    public class FlightDestinationsResponse
    {
        [JsonPropertyName("data")]
        public List<FlightDestination> Data { get; set; }
    }

    public class FlightDestination
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("departureDate")]
        public DateTime DepartureDate { get; set; }

        [JsonPropertyName("returnDate")]
        public DateTime ReturnDate { get; set; }

        [JsonPropertyName("price")]
        public Price Price { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("total")]
        public string Total { get; set; }

        public decimal TotalAsDecimal => decimal.Parse(Total, System.Globalization.CultureInfo.InvariantCulture);
    }

}
