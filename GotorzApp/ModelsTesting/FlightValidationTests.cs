using System.ComponentModel.DataAnnotations;
using SharedLib;

namespace ModelsTesting;

public class FlightValidationTests
{
    private IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void Should_PassValidation_When_MinTimeDifferenceIsValid()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(1).AddHours(8),
            ArrivalTime = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(1),
            IataOriginId = 1,
            IataDestinationId = 2,
            IataOrigin = new IataLocation() { Id = 1, Iata = "LAX", City = "Los Angeles" },
            IataDestination = new IataLocation() { Id = 2, Iata = "CPH", City = "Copenhagen" }
        };

        var results = ValidateModel(flight);

        Assert.Empty(results);
    }

    [Fact]
    public void Should_PassValidation_When_MaxTimeDifferenceIsValid()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(2),
            ArrivalTime = DateTime.Today.AddDays(4), // exactly 48 hours later
            IataOriginId = 1,
            IataDestinationId = 2,
            IataOrigin = new IataLocation() { Id = 1, Iata = "LAX", City = "Los Angeles" },
            IataDestination = new IataLocation() { Id = 2, Iata = "CPH", City = "Copenhagen" }
        };

        var results = ValidateModel(flight);

        Assert.Empty(results);
    }

    [Fact]
    public void Should_FailValidation_When_TimeDifferenceExceedsLimit()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(1),
            ArrivalTime = DateTime.Today.AddDays(3).AddHours(1), // 49 hours later
            IataOriginId = 1,
            IataDestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("within 48 hours"));
    }

    [Fact]
    public void Should_FailValidation_When_ArrivalIsBeforeDeparture()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(5),
            ArrivalTime = DateTime.Today.AddDays(4),
            IataOriginId = 1,
            IataDestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("after departure"));
    }

    [Fact]
    public void Should_FailValidation_When_DepartureIsBeyondOneYear()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddYears(1).AddDays(1),
            ArrivalTime = DateTime.Today.AddYears(1).AddDays(1).AddHours(1),
            IataOriginId = 1,
            IataDestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("within one year"));
    }

    [Fact]
    public void Should_FailValidation_When_IataLocationsAreTheSame()
    {
        var flight = new Flight
        {
            Id = 1,
            DepartureTime = DateTime.Today.AddDays(1),
            ArrivalTime = DateTime.Today.AddDays(1).AddHours(1),
            IataOrigin = new IataLocation() { Id = 1, Iata = "LAX", City = "Los Angeles" },
            IataDestination = new IataLocation() { Id = 1, Iata = "LAX", City = "Los Angeles" }
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("IataOrigin and IataDestination cannot be the same."));
    }
}