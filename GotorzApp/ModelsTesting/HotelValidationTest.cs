using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsTesting;

public class HotelTests
{
    private List<ValidationResult> ValidateModel(Hotel hotel)
    {
        var context = new ValidationContext(hotel);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(hotel, context, results, true);
        return results;
    }

    [Fact]
    public void Should_PassValidation_When_AllFieldsAreValid()
    {
        var hotel = CreateValidHotel();

        var results = ValidateModel(hotel);

        Assert.Empty(results);
    }

    [Fact]
    public void Should_PassValidation_When_CheckInIsTodayAndCheckOutIsTomorrow()
    {
        var hotel = CreateValidHotel();
        hotel.CheckIn = DateTime.Today;
        hotel.CheckOut = DateTime.Today.AddDays(1);

        var results = ValidateModel(hotel);

        Assert.Empty(results);
    }

    [Fact]
    public void Should_PassValidation_When_CheckInIsExactlyOneYearFromToday()
    {
        var hotel = CreateValidHotel();
        hotel.CheckIn = DateTime.Today.AddYears(1);
        hotel.CheckOut = hotel.CheckIn.AddDays(1);

        var results = ValidateModel(hotel);

        Assert.Empty(results);
    }

    [Fact]
    public void Should_FailValidation_When_RateIsNegative()
    {
        var hotel = CreateValidHotel();
        hotel.Rate = -100;

        var results = ValidateModel(hotel);

        Assert.Contains(results, r => r.ErrorMessage == "Rate cannot be negative.");
    }

    [Fact]
    public void Should_FailValidation_When_PhoneNumberIsInvalid()
    {
        var hotel = CreateValidHotel();
        hotel.Telephonenumber = "invalid-phone";

        var results = ValidateModel(hotel);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(hotel.Telephonenumber)));
    }

    [Fact]
    public void Should_FailValidation_When_EmailIsInvalid()
    {
        var hotel = CreateValidHotel();
        hotel.Email = "not-an-email";

        var results = ValidateModel(hotel);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(hotel.Email)));
    }

    [Fact]
    public void Should_FailValidation_When_CheckOutIsBeforeCheckIn()
    {
        var hotel = CreateValidHotel();
        hotel.CheckOut = hotel.CheckIn.AddDays(-1);

        var results = ValidateModel(hotel);

        Assert.Contains(results, r => r.ErrorMessage == "Check-out date must be after check-in date.");
    }

    [Fact]
    public void Should_FailValidation_When_CheckInIsAfterOneYear()
    {
        var hotel = CreateValidHotel();
        hotel.CheckIn = DateTime.Today.AddYears(1).AddDays(1);
        hotel.CheckOut = hotel.CheckIn.AddDays(1);

        var results = ValidateModel(hotel);

        Assert.Contains(results, r => r.ErrorMessage == "Check-in date must be between today and within the next year.");
    }

    [Fact]
    public void Should_FailValidation_When_CheckInIsBeforeToday()
    {
        var hotel = CreateValidHotel();
        hotel.CheckIn = DateTime.Today.AddDays(-1);

        var results = ValidateModel(hotel);

        Assert.Contains(results, r => r.ErrorMessage == "Check-in date must be between today and within the next year.");
    }

    [Fact]
    public void Should_FailValidation_When_RequiredFieldsAreMissing()
    {
        var hotel = new Hotel();

        var results = ValidateModel(hotel);

        Assert.NotEmpty(results);
    }

    private Hotel CreateValidHotel()
    {
        return new Hotel
        {
            CheckIn = DateTime.Today.AddDays(1),
            CheckOut = DateTime.Today.AddDays(2),
            Rate = 150.0m,
            Name = "Test Hotel",
            Address = "12A Main Street, 12345 Test City, Testland",
            Telephonenumber = "123-456-7890",
            Email = "test@example.com",
            Description = "A nice test hotel."
        };
    }
}