using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class AppliedJob
{
    public Guid AppliedJobId { get; set; }

    public Guid JobPostId { get; set; }

    public Guid JobSeekerProfileId { get; set; }

    public DateTime AppliedDate { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual JobPost JobPost { get; set; } = null!;

    public virtual JobSeekerProfile JobSeekerProfile { get; set; } = null!;
}
