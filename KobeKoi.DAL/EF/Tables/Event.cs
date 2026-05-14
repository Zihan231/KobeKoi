using System;
using System.Collections.Generic;

namespace KobeKoi.DAL.EF.Tables;

public partial class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public int MaxCapacity { get; set; }

    public int AvailableSeats { get; set; }

    public int Status { get; set; }

    public int VenuePaymentStatus { get; set; }

    public int VenueId { get; set; }

    public int OrganizerId { get; set; }

    public decimal TicketPrice { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User Organizer { get; set; } = null!;

    public virtual Venue Venue { get; set; } = null!;
}
