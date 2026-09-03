using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SystemUser
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int Roles { get; set; }

    public int Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<AuthUser> AuthUsers { get; set; } = new List<AuthUser>();

    public virtual ICollection<JobProvider> JobProviders { get; set; } = new List<JobProvider>();

    public virtual ICollection<JobSeeker> JobSeekers { get; set; } = new List<JobSeeker>();
}
