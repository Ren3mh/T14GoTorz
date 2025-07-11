﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace SharedLib;

// vores Hotel model
public partial class Hotel
{
    public int Id { get; set; }

    [Required]
    [CustomValidation(typeof(HotelValidator), "ValidateCheckIn")]
    public DateTime CheckIn { get; set; }

    [Required]
    [CustomValidation(typeof(HotelValidator), "ValidateCheckOut")]
    public DateTime CheckOut { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Rate cannot be negative.")]
    public decimal Rate { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string Telephonenumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? Description { get; set; }

    public static string CreateAddress(string streetNumber, string street, string zipCode, string city, string country, string other)
    {
        var _other = other == "" ? null : $"({other}) ";
        var hotelAddress = $"{streetNumber} {_other}{street}, {zipCode} {city}, {country}";
        return hotelAddress;
    }
}

public static class HotelValidator
{
    public static ValidationResult? ValidateCheckIn(object? value, ValidationContext context)
    {
        if (value is DateTime checkInDate)
        {
            var now = DateTime.Today;
            if (checkInDate < now || checkInDate > now.AddYears(1))
            {
                return new ValidationResult("Check-in date must be between today and within the next year.");
            }
        }

        return ValidationResult.Success;
    }

    public static ValidationResult? ValidateCheckOut(object? value, ValidationContext context)
    {
        if (context.ObjectInstance is Hotel hotel)
        {
            if (hotel.CheckOut <= hotel.CheckIn)
            {
                return new ValidationResult("Check-out date must be after check-in date.");
            }
        }

        return ValidationResult.Success;
    }
}

// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
public partial class Hotel
{
    [JsonIgnore]
    public virtual ICollection<TravelPackage> TravelPackages { get; set; } = new List<TravelPackage>();
}