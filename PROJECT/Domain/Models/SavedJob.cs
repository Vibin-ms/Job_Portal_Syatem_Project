using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SavedJob
{
    public Guid SavedJobId { get; set; }

    public Guid JobPostId { get; set; }

    public Guid JobSeekerProfileId { get; set; }

    public DateTime SavedDate { get; set; }

    public virtual JobPost JobPost { get; set; } = null!;

    public virtual JobSeekerProfile JobSeekerProfile { get; set; } = null!;
}
