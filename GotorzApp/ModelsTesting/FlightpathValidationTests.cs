using Shared;
using System.ComponentModel.DataAnnotations;

namespace ModelsTesting;

public class FlightpathValidationTests
{
    private IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }

    private Flight CreateFlight(DateTime dep, DateTime arr)
    {
        return new Flight
        {
            DepartureTime = dep,
            ArrivalTime = arr,
            IataOriginId = 1,
            IataDestinationId = 2
        };
    }

    // --------------------- MIN TESTS ---------------------

    [Fact]
    public void Should_PassValidation_When_FareIsZero()
    {
        var path = new Flightpath
        {
            Fare = 0,
            Luggage = true,
            OutboundFlight = CreateFlight(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(1)),
            HomeboundFlight = CreateFlight(DateTime.Today.AddDays(2), DateTime.Today.AddDays(2).AddHours(1))
        };

        var results = ValidateModel(path);
        Assert.Empty(results);
    }

    // --------------------- MAX TESTS ---------------------

    [Fact]
    public void Should_PassValidation_When_FareIsDecimalMax()
    {
        var path = new Flightpath
        {
            Fare = decimal.MaxValue,
            Luggage = true,
            OutboundFlight = CreateFlight(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(1)),
            HomeboundFlight = CreateFlight(DateTime.Today.AddDays(2), DateTime.Today.AddDays(2).AddHours(1))
        };

        var results = ValidateModel(path);
        Assert.Empty(results);
    }

    // ------------------ EXCEPTION TESTS ------------------

    [Fact]
    public void Should_FailValidation_When_FareIsNegative()
    {
        var path = new Flightpath
        {
            Fare = -0.01m,
            Luggage = true,
            OutboundFlight = CreateFlight(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(1)),
            HomeboundFlight = CreateFlight(DateTime.Today.AddDays(2), DateTime.Today.AddDays(2).AddHours(1))
        };

        var results = ValidateModel(path);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Fare cannot be negative"));
    }

    [Fact]
    public void Should_FailValidation_When_HomeboundDepartureIsBeforeOutboundArrival()
    {
        var path = new Flightpath
        {
            Fare = 100,
            Luggage = true,
            OutboundFlight = CreateFlight(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(5)),
            HomeboundFlight = CreateFlight(DateTime.Today.AddDays(1).AddHours(4), DateTime.Today.AddDays(1).AddHours(6))
        };

        var results = ValidateModel(path);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Homebound Departure Time cannot be before Outbound Flight Arrival Time"));
    }

    [Fact]
    public void Should_FailValidation_When_FlightsAreMissing()
    {
        var path = new Flightpath
        {
            Fare = 100,
            Luggage = true,
            OutboundFlight = null,
            HomeboundFlight = null
        };

        var results = ValidateModel(path);
        Assert.Contains(results, r => r.MemberNames.Contains("OutboundFlight"));
        Assert.Contains(results, r => r.MemberNames.Contains("HomeboundFlight"));
    }
}