using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.DTO
{
    public class AdminStatsDTO
    {
        public int TotalUsers { get; set; }
        public int TotalEvents { get; set; }

        public int PendingEvents { get; set; }
        public int LiveEvents { get; set; }
        public int RejectedEvents { get; set; }

        public int LockedUsers { get; set; }
        public List<EventDTO> RecentEvents { get; set; }
        public List<UserDTO> RecentUsers { get; set; }
    }
}
