using System;

namespace Domain.Services.JobApplications.DTO
{
    public class JobApplicationDto
    {
        public Guid JobApplicationId { get; set; }
        public Guid JobPostId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public Guid AppliedJobId { get; set; }
        public Guid JobSeekerProfileId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string CandidateEmail { get; set; } = string.Empty;
        public string CandidatePhone { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }
        public int ApplicationStatus { get; set; }
        public string ApplicationStatusName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
    }

    public class UpdateApplicationStatusDto
    {
        public int Status { get; set; }
    }

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
        public string ApplicationStatus { get; set; } = null!;
        public DateTime ApplicationDate { get; set; }
    }
}
