using System;
using System.Collections.Generic;

namespace KobeKoi.DAL.EF.Tables;

public partial class Venue
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int CapacityLimit { get; set; }

    public decimal RentalPricePerDay { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
