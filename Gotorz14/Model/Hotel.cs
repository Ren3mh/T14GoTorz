namespace Gotorz14.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Rate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } // finder vi ud af senere.
        public string Meals { get; set; }
    }
}
