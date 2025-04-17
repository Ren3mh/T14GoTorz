using System.ComponentModel.DataAnnotations;

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
    public void ValidFlight_MinTimeDifference_ShouldPassValidation()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(1).AddHours(8),
            ArrivalTime = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(1),
            IataoriginId = 1,
            IatadestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Empty(results);
    }

    [Fact]
    public void ValidFlight_MaxTimeDifference_ShouldPassValidation()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(2),
            ArrivalTime = DateTime.Today.AddDays(4), // exactly 48 hours later
            IataoriginId = 1,
            IatadestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Empty(results);
    }

    [Fact]
    public void InvalidFlight_TooMuchTimeDifference_ShouldFailValidation()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(1),
            ArrivalTime = DateTime.Today.AddDays(3).AddHours(1), // 49 hours later
            IataoriginId = 1,
            IatadestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("within 48 hours"));
    }

    [Fact]
    public void InvalidFlight_ArrivalBeforeDeparture_ShouldFailValidation()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(5),
            ArrivalTime = DateTime.Today.AddDays(4),
            IataoriginId = 1,
            IatadestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("after departure"));
    }

    [Fact]
    public void InvalidFlight_DepartureBeyondOneYear_ShouldFailValidation()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddYears(1).AddDays(1),
            ArrivalTime = DateTime.Today.AddYears(1).AddDays(1).AddHours(1),
            IataoriginId = 1,
            IatadestinationId = 2
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("within one year"));
    }

    [Fact]
    public void InvalidFlight_NegativeIataIds_ShouldFailValidation()
    {
        var flight = new Flight
        {
            DepartureTime = DateTime.Today.AddDays(1),
            ArrivalTime = DateTime.Today.AddDays(1).AddHours(1),
            IataoriginId = 0,
            IatadestinationId = -5
        };

        var results = ValidateModel(flight);

        Assert.Contains(results, r => r.ErrorMessage.Contains("IATA Origin ID cannot be negative"));
        Assert.Contains(results, r => r.ErrorMessage.Contains("IATA Destination ID cannot be negative"));
    }
}