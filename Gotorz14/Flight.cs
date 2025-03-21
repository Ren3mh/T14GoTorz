using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gotorz14;

[Keyless]
public partial class Flight
{
    public int Id { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string? Destination { get; set; }

    public string? Origin { get; set; }

    [StringLength(3)]
    public string IataDestination { get; set; } = null!;

    [StringLength(3)]
    public string IataOrigin { get; set; } = null!;
}
