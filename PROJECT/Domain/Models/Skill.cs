using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Skill
{
    public Guid SkillId { get; set; }

    public string SkillName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<JobSeekerProfile> JobSeekerProfiles { get; set; } = new List<JobSeekerProfile>();
}
