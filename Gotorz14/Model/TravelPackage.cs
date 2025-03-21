using System.ComponentModel.DataAnnotations;

namespace Gotorz14.Model
{
    public class TravelPackage
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImagePath { get; set; }
        public List<Flightpath> Flightpaths { get; set; }
        public Hotel Hotel { get; set; }
        public Transfer ToHotel { get; set; }
        public Transfer ToAirport { get; set; }
    }
}
