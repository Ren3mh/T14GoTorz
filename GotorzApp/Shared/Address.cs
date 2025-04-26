using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class Address
{
    [Required]
    public string StreetNumber { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string ZipCode { get; set; }

    [Required]
    public string Country { get; set; }

    public string? Other { get; set; }

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
