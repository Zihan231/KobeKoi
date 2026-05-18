using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KobeKoi.BLL.DTO
{
    public class CreateEventDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Capacity must be greater than 0")]
        public int MaxCapacity { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public int OrganizerId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Ticket price must be greater than 0")]
        public decimal TicketPrice { get; set; }
    }
}
