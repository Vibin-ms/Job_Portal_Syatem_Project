using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class JobType
{
    public Guid JobTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
}
