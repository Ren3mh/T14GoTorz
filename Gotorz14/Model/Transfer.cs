namespace Gotorz14.Model
{
    public class Transfer
    {
        public string From { get; set; } // address
        public string To { get; set; } // address
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Fare { get; set; }
    }
}
