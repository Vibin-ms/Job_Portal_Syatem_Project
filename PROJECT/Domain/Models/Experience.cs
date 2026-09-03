using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Experience
{
    public Guid ExperienceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<JobSeekerProfile> JobSeekerProfiles { get; set; } = new List<JobSeekerProfile>();
}
