using Domain.Enum;

namespace Job_Portal_System.API.AdminController.Request___Response_Body
{
    public class ApplicationResponse
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
