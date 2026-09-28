using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Services.JobPost.DTO
{
    public class JobPostDTO
    {
        public Guid JobPostId { get; set; }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public decimal Salary { get; set; }


        public string Company { get; set; }

        public string Jobcategory { get; set; }

        public string Location { get; set; }

        public string JobType { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime DeadLine { get; set; }

        public string Status { get; set; }

    }


    public class JobPostResponseDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid JobPostId { get => Id; set => Id = value; }
        public Guid JobProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public Guid JobTypeId { get; set; }
        public string JobTypeName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public int ExperienceRequiredYears { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime Deadline { get; set; }
        [JsonIgnore]
        public DateTime DeadLine { get => Deadline; set => Deadline = value; }
        public JobPostStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get => CreatedAt; set => CreatedAt = value; }
        public int TotalApplications { get; set; }
    }

    public class JobPostSummaryDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid JobPostId { get => Id; set => Id = value; }
        public string Title { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public EmploymentType EmploymentType { get; set; }
        public decimal? Salary { get; set; }
        public int ExperienceRequiredYears { get; set; }
        public DateTime Deadline { get; set; }
        [JsonIgnore]
        public DateTime DeadLine { get => Deadline; set => Deadline = value; }
        public JobPostStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get => CreatedAt; set => CreatedAt = value; }
    }

    public class CreateJobPostDto
    {
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public Guid? JobcategoryId { get => CategoryId; set => CategoryId = value; }
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
        [JsonIgnore]
        public DateTime DeadLine { get => Deadline; set => Deadline = value; }
        public JobPostStatus Status { get; set; } = JobPostStatus.Accepted;
    }

    public class UpdateJobPostDto
    {
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public Guid? JobcategoryId { get => CategoryId; set => CategoryId = value; }
        public Guid? LocationId { get; set; }
        public Guid? JobTypeId { get; set; }
        public Guid? ExperienceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int ExperienceRequiredYears { get; set; }
        public DateTime Deadline { get; set; }
        [JsonIgnore]
        public DateTime DeadLine { get => Deadline; set => Deadline = value; }
    }
}

