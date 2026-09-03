using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class JobSeekerProfile
{
    public Guid JobSeekerProfileId { get; set; }

    public Guid JobSeekerId { get; set; }

    public string? About { get; set; }

    public string? ResumeUrl { get; set; }

    public Guid SkillId { get; set; }

    public Guid QualificationId { get; set; }

    public Guid ExperienceId { get; set; }

    public Guid LocationId { get; set; }

    public virtual ICollection<AppliedJob> AppliedJobs { get; set; } = new List<AppliedJob>();

    public virtual Experience Experience { get; set; } = null!;

    public virtual JobSeeker JobSeeker { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Qualification Qualification { get; set; } = null!;

    public virtual ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();

    public virtual Skill Skill { get; set; } = null!;
}
