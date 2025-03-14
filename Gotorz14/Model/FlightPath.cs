namespace Gotorz14.Model
{
    public class FlightPath
    {
        public double Fare { get; set; }
        public bool Luggage {  get; set; }

        public Fligth Outbound {  get; set; }
        public Fligth Homebound { get; set; }

    }
}
