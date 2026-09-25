using Domain.Enum;
using System;

namespace Job_Portal_System.API.JobProviderController.JobPost.Request___Response_Body
{
    public class CreateJobRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? JobTypeId { get; set; }
        public Guid? ExperienceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public EmploymentType EmploymentType { get; set; } = EmploymentType.FullTime;
        public int ExperienceRequiredYears { get; set; }
        public int Vacancies { get; set; } = 1;
        public DateTime Deadline { get; set; }
        public JobPostStatus Status { get; set; } = JobPostStatus.Accepted;
    }

    public class UpdateJobRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? JobTypeId { get; set; }
        public Guid? ExperienceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int ExperienceRequiredYears { get; set; }
        public DateTime Deadline { get; set; }
    }
}
