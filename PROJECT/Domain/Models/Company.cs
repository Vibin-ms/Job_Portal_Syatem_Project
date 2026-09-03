using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Company
{
    public Guid CompanyId { get; set; }

    public Guid IndustryId { get; set; }

    public Guid LocationId { get; set; }

    public string ComapnyName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

    public virtual Industry Industry { get; set; } = null!;

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual Location Location { get; set; } = null!;
}
