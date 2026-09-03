using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class AuthUser
{
    public Guid AuthUserId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public Guid SystemUserId { get; set; }

    public virtual SystemUser SystemUser { get; set; } = null!;
}
