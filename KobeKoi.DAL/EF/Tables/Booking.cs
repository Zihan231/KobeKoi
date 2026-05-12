using System;
using System.Collections.Generic;

namespace KobeKoi.DAL.EF.Tables;

public partial class Booking
{
    public int Id { get; set; }

    public string BookingReference { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public int PaymentStatus { get; set; }

    public DateTime BookingDate { get; set; }

    public int EventId { get; set; }

    public int AttendeeId { get; set; }

    public virtual User Attendee { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
