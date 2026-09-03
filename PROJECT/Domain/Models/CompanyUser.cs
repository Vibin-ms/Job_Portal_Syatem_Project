using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class CompanyUser
{
    public Guid CompanyUserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid JobProviderId { get; set; }

    public int Roles { get; set; }

    public int Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual JobProvider JobProvider { get; set; } = null!;
}
