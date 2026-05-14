using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.DTO
{
    public class CreateUserDTO
    {
        public string? Name { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Role { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
