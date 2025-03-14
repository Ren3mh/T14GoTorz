using System.Text.Json.Serialization;

namespace Gotorz14.Model
{
    public class Fligth
    {
        public DateTime DepartureTime {  get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Destination {  get; set; }
        public string Origin { get; set; }
        public string IataDestination { get; set; }
        public string IataOrigin { get; set; }
    }
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
}
