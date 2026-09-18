using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.DTO
{
    public class ApplicationDTO
    {
        public Guid JobApplicationId { get; set; }

        public string JobTitle { get; set; } = null!;

        public string JobDescription { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? ResumeUrl { get; set; }

        public string? SkillName { get; set; }

        public string? QualificationName { get; set; }

        public string? ExperienceName { get; set; }

        public string ApplicationStatus { get; set; }

        public DateTime ApplicationDate { get; set; }
    }
}
