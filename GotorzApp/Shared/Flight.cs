using Shared;
using System.ComponentModel.DataAnnotations;

public partial class Flight
{

    public int Id { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [FutureDateWithinYear(ErrorMessage = "Departure time must be within one year from today.")]
    public DateTime DepartureTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [FutureDateWithinYear(ErrorMessage = "Arrival time must be within one year from today.")]
    public DateTime ArrivalTime { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "IATA Destination ID cannot be negative.")]
    public int IatadestinationId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "IATA Origin ID cannot be negative.")]
    public int IataoriginId { get; set; }

    public virtual ICollection<Flightpath> FlightpathHomeboundFlights { get; set; }

    public virtual ICollection<Flightpath> FlightpathOutboundFlights { get; set; }

    public virtual IataLocation? Iatadestination { get; set; }

    public virtual IataLocation? Iataorigin { get; set; }

    public Flight()
    {
        Id = -1;
        FlightpathHomeboundFlights = new List<Flightpath>();
        FlightpathOutboundFlights = new List<Flightpath>();
        Iataorigin = null;
        Iatadestination = null;
    }
}

// Custom validation attribute to ensure the date is within one year from today
public class FutureDateWithinYearAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date > DateTime.Today && date < DateTime.Today.AddYears(1);
        }
        return false;
    }
}