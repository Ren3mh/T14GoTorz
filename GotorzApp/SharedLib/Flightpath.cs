using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedLib;

// vores Flightpath model
[CustomValidation(typeof(FlightpathValidator), "ValidateFlightTimes")]
public partial class Flightpath
{
    public int Id { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Fare cannot be negative.")]
    public decimal Fare { get; set; }

    [Required]
    public bool Luggage { get; set; }

    [Required]
    public virtual Flight OutboundFlight { get; set; }

    [Required]
    public virtual Flight HomeboundFlight { get; set; }
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

        // Check if Homebound Departure Time is before Outbound Flight Arrival Time
        if (flightpath.HomeboundFlight.DepartureTime < flightpath.OutboundFlight.ArrivalTime)
        {
            return new ValidationResult("Homebound Departure Time cannot be before Outbound Flight Arrival Time.");
        }

        // Check if Outbound and Homebound IataLocation match
        if (flightpath.OutboundFlight.IataDestination != flightpath.HomeboundFlight.IataOrigin ||
            flightpath.OutboundFlight.IataOrigin != flightpath.HomeboundFlight.IataDestination)
        {
            return new ValidationResult("Outbound and Homebound flights should have the oposite IataLocations.");
        }

        return ValidationResult.Success;
    }
}

// datacontext Flithpath model
public partial class Flightpath
{
    [JsonIgnore]
    public virtual int OutboundFlightId { get; set; }

    [JsonIgnore]
    public virtual int HomeboundFlightId { get; set; }

    [JsonIgnore]
    public virtual int TravelPackageId { get; set; }

    [JsonIgnore]
    public virtual TravelPackage? TravelPackage { get; set; }
}