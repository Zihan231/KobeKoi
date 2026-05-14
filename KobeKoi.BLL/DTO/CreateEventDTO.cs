using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.DTO
{
    internal class CreateEventDTO
    {
        public string Title { get; set; } = null!;

        public DateTime EventDate { get; set; }

        public int MaxCapacity { get; set; }

        public int VenueId { get; set; }

        public int OrganizerId { get; set; }
    }
}
