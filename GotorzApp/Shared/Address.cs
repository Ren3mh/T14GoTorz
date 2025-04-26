using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class Address
{
    private string streetNumber;
    private string street;
    private string city;
    private string zipCode;
    private string country;
    private string? other;

    [Required]
    public string StreetNumber
    {
        get => streetNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Street number cannot be null or empty.", nameof(StreetNumber));
            streetNumber = value;
        }
    }

    [Required]
    public string Street
    {
        get => street;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Street cannot be null or empty.", nameof(Street));
            street = value;
        }
    }

    [Required]
    public string City
    {
        get => city;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("City cannot be null or empty.", nameof(City));
            city = value;
        }
    }

    [Required]
    public string ZipCode
    {
        get => zipCode;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Zip code cannot be null or empty.", nameof(ZipCode));
            zipCode = value;
        }
    }

    [Required]
    public string Country
    {
        get => country;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Country cannot be null or empty.", nameof(Country));
            country = value;
        }
    }

    public string? Other
    {
        get => other;
        set
        {
            if (value == "")
                throw new ArgumentException("Other cannot be empty.", nameof(Other));
            other = value;
        }
    }

    public Address(string streetNumber, string street, string city, string zipCode, string country, string? other = null)
    {
        StreetNumber = streetNumber;
        Street = street;
        City = city;
        ZipCode = zipCode;
        Country = country;
        Other = other;
    }

    public override string ToString()
    {
        var _other = string.IsNullOrEmpty(Other) ? "" : $"({Other}) ";
        return $"{StreetNumber} {_other}{Street}, {ZipCode} {City}, {Country}";
    }
}
