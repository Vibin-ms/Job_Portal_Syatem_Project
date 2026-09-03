using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class JobSeeker
{
    public Guid JobSeekerId { get; set; }

    public Guid SystemUserId { get; set; }

    public virtual ICollection<JobSeekerProfile> JobSeekerProfiles { get; set; } = new List<JobSeekerProfile>();

    public virtual SystemUser SystemUser { get; set; } = null!;
}
