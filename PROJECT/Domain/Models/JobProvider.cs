using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class JobProvider
{
    public Guid JobProviderId { get; set; }

    public Guid SystemUserId { get; set; }
    public string Designation { get; set; } = string.Empty;

    public virtual SystemUser SystemUser { get; set; } = null!;

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
