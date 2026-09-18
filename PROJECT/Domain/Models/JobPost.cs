using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class JobPost
{
    public Guid JobPostId { get; set; }

    public Guid JobProviderId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid JobcategoryId { get; set; }

    public Guid LocationId { get; set; }

    public Guid JobTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public DateTime DeadLine { get; set; }

    public JobStatus Status { get; set; }

    public virtual ICollection<AppliedJob> AppliedJobs { get; set; } = new List<AppliedJob>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual JobProvider JobProvider { get; set; } = null!;

    public virtual JobType JobType { get; set; } = null!;

    public virtual JobCategory Jobcategory { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();
}
