using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.DTO
{
    public class EventDTO
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
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
    }
}
