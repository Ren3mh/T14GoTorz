//using System.Text.Json.Serialization;
//using System.Text.Json;

//namespace Gotorz14.Services
//{
//    //public class ReneRod
//    //{
//    //    NotImplementedException;
//    //}

//    public DateTime? ArrivalOfCheapestFlight(FareData fareData)
//    {
//        DateTime? ArrivalDateTimeOfCheapestFlight = fareData.Outbound.MinFare.ArrivalDate;

//        //Vælg den billigste og vælg ArrivalDate
//        //Brug denne til at søge efter hoteller fra den dato ...

//        return ArrivalDateTimeOfCheapestFlight;
//    }

//    public static async Task<FareData> MakeFlightsList()
//    {
//        // URL til API
//        string url = "https://www.ryanair.com/api/farfnd/v4/oneWayFares/BER/BRU/cheapestPerDay?outboundMonthOfDate=2025-03-01&currency=EUR";

//        using (HttpClient client = new HttpClient())
//        {
//            // Udfør API-kaldet
//            HttpResponseMessage response = await client.GetAsync(url);

//            if (response.IsSuccessStatusCode)
//            {
//                // Læs JSON-svaret som en string


//                string jsonResponse = await response.Content.ReadAsStringAsync();
//                FareData fareData = JsonSerializer.Deserialize<FareData>(jsonResponse);




//                return fareData;
//            }
//            else
//            {
//                return new FareData();
//            }
//        }
//    }



//    public class FareData
//    {
//        [JsonPropertyName("outbound")]
//        public Outbound Outbound { get; set; }
//    }

//    public class Outbound
//    {
//        [JsonPropertyName("fares")]
//        public List<Fare> Fares { get; set; }

//        [JsonPropertyName("minFare")]
//        public Fare MinFare { get; set; }

//        [JsonPropertyName("maxFare")]
//        public Fare MaxFare { get; set; }
//    }

//    public class Fare
//    {
//        [JsonPropertyName("day")]
//        public string Day { get; set; }

//        [JsonPropertyName("arrivalDate")]
//        public DateTime? ArrivalDate { get; set; }

//        [JsonPropertyName("departureDate")]
//        public DateTime? DepartureDate { get; set; }

//        [JsonPropertyName("price")]
//        public Price Price { get; set; }

//        [JsonPropertyName("soldOut")]
//        public bool SoldOut { get; set; }

//        [JsonPropertyName("unavailable")]
//        public bool Unavailable { get; set; }
//    }

//    public class Price
//    {
//        [JsonPropertyName("value")]
//        public double Value { get; set; }

//        [JsonPropertyName("valueMainUnit")]
//        public string ValueMainUnit { get; set; }

//        [JsonPropertyName("valueFractionalUnit")]
//        public string ValueFractionalUnit { get; set; }

//        [JsonPropertyName("currencyCode")]
//        public string CurrencyCode { get; set; }

//        [JsonPropertyName("currencySymbol")]
//        public string CurrencySymbol { get; set; }
//    }
//}
