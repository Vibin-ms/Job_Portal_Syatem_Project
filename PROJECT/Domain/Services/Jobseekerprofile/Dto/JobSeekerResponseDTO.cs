using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseekerprofile.Dto
{
    public class JobSeekerResponseDTO
    {
        public Guid Id { get; set; }
        public Guid JobSeekerProfileId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string? About { get; set; }
        public string? ResumeUrl { get; set; }

        public string? SkillName { get; set; }
        public string? QualificationName { get; set; }
        public string? ExperienceName { get; set; }
        public string? LocationName { get; set; }

    }
}
