using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Authentication.Dto
{
   public class SystemuserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        

        public Role? Roles { get; set; }

        public Status? Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
