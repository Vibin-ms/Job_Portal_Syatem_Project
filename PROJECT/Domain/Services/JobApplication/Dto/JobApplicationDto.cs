using System;

namespace Domain.Services.JobApplication.Dto
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
}
