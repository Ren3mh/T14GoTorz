using SharedLib;
using System.ComponentModel.DataAnnotations;

namespace SharedLib;

// vores Flight model
[CustomValidation(typeof(FlightValidator), "ValidateIataLocations")]
public partial class Flight
{
    public int Id { get; set; }

    [Required]
    [FutureDateWithinYear(ErrorMessage = "Departure time must be within one year from today.")]
    public DateTime DepartureTime { get; set; }

    [Required]
    [FutureDateWithinYear(ErrorMessage = "Arrival time must be within one year from today.")]
    [ArrivalAfterDepartureWithinLimit("DepartureTime", ErrorMessage = "Arrival must be after departure and within 48 hours.")]
    public DateTime ArrivalTime { get; set; }

    [Required]
    public IataLocation IataDestination { get; set; }

    [Required]
    public IataLocation IataOrigin { get; set; }

}

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

public static class FlightValidator
{
    public static ValidationResult ValidateIataLocations(object value, ValidationContext context)
    {
        var flight = value as Flight;

        if (flight == null || flight.IataOrigin == null || flight.IataDestination == null)
        {
            return ValidationResult.Success;
        }

        if (flight.IataOrigin.Id == flight.IataDestination.Id)
        {
            return new ValidationResult("IataOrigin and IataDestination cannot be the same.");
        }

        return ValidationResult.Success;
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

// dataContext Flight model
public partial class Flight
{
    public virtual ICollection<Flightpath> FlightpathHomeboundFlights { get; set; }

    public virtual ICollection<Flightpath> FlightpathOutboundFlights { get; set; }

    public int IataDestinationId { get; set; }

    public int IataOriginId { get; set; }
}