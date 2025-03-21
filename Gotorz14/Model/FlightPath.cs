using System.ComponentModel.DataAnnotations.Schema;

namespace Gotorz14.Model
{
    public class Flightpath
    {
        public int Id { get; set; }
        public double Fare { get; set; }
        public bool Luggage { get; set; }

        public Fligth Outbound {  get; set; }
        public Fligth Homebound { get; set; }
    }
}
