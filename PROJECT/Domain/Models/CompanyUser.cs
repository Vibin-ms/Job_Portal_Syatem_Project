using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class CompanyUser
{
    public Guid CompanyUserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid JobProviderId { get; set; }

    public string Designation { get; set; } = string.Empty;
    public string RoleInCompany { get; set; } = "Recruiter";
    public int Status { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual Company Company { get; set; } = null!;

    public virtual JobProvider JobProvider { get; set; } = null!;
}
