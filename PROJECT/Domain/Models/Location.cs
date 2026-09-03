using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Location
{
    public Guid LocationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual ICollection<JobSeekerProfile> JobSeekerProfiles { get; set; } = new List<JobSeekerProfile>();
}
