using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CompanyProfile.DTO
{
    public class CompanyProfileDTO
    {
        public Guid CompanyId { get; set; }

        public string Industry { get; set; }

        public string Location { get; set; }

        public string ComapnyName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string Status { get; set; }

    }
}
