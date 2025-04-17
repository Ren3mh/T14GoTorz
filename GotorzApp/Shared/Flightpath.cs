using System;
using System.ComponentModel.DataAnnotations;

namespace Shared;

[CustomValidation(typeof(FlightpathValidator), "ValidateFlightTimes")]
public partial class Flightpath
{
    public int Id { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Fare cannot be negative.")]
    public decimal Fare { get; set; }

    [Required]
    public bool Luggage { get; set; }

    public int OutboundFlightId { get; set; }

    public int HomeboundFlightId { get; set; }

    public int TravelPackageId { get; set; }

    [Required]
    public virtual Flight OutboundFlight { get; set; }

    [Required]
    public virtual Flight HomeboundFlight { get; set; }

    public virtual TravelPackage TravelPackage { get; set; }
}

public static class FlightpathValidator
{
    public static ValidationResult ValidateFlightTimes(object value, ValidationContext context)
    {
        var flightpath = value as Flightpath;

        if (flightpath == null || flightpath.HomeboundFlight == null || flightpath.OutboundFlight == null)
        {
            return ValidationResult.Success;
        }

        if (flightpath.HomeboundFlight.DepartureTime < flightpath.OutboundFlight.ArrivalTime)
        {
            return new ValidationResult("Homebound Departure Time cannot be before Outbound Flight Arrival Time.");
        }

        return ValidationResult.Success;
    }
}