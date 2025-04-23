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
    [ArrivalAfterDepartureWithinLimit("DepartureTime", ErrorMessage = "Arrival must be after departure and within 48 hours.")]
    public DateTime ArrivalTime { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "IATA Destination ID cannot be negative.")]
    public int IatadestinationId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "IATA Origin ID cannot be negative.")]
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

public class ArrivalAfterDepartureWithinLimitAttribute : ValidationAttribute
{
    private readonly string _departureTimePropertyName;
    private readonly double _maxHours;

    public ArrivalAfterDepartureWithinLimitAttribute(string departureTimePropertyName, double maxHours = 48)
    {
        _departureTimePropertyName = departureTimePropertyName;
        _maxHours = maxHours;
        ErrorMessage = $"Arrival time must be after departure time and within {_maxHours} hours.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var arrivalTime = value as DateTime?;
        var departureTimeProperty = validationContext.ObjectType.GetProperty(_departureTimePropertyName);
        if (departureTimeProperty == null)
        {
            return new ValidationResult($"Unknown property: {_departureTimePropertyName}");
        }

        var departureTime = departureTimeProperty.GetValue(validationContext.ObjectInstance) as DateTime?;

        if (!arrivalTime.HasValue || !departureTime.HasValue)
        {
            return ValidationResult.Success; // Let [Required] handle nulls
        }

        if (arrivalTime <= departureTime)
        {
            return new ValidationResult("Arrival time must be after departure time.");
        }

        if ((arrivalTime - departureTime)?.TotalHours > _maxHours)
        {
            return new ValidationResult($"Arrival time must be within {_maxHours} hours of departure.");
        }

        return ValidationResult.Success;
    }
}