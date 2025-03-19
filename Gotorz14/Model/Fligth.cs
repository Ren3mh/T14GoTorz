using System.Text.Json.Serialization;

namespace Gotorz14.Model
{
    public class Fligth
    {
        public int Id { get; set; }
        public DateTime DepartureTime {  get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Destination {  get; set; }
        public string Origin { get; set; }
        public string IataDestination { get; set; }
        public string IataOrigin { get; set; }
    }
}
