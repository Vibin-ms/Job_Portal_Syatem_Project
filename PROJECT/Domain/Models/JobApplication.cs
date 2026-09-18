using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class JobApplication
{
    public Guid JobApplicationId { get; set; }

    public Guid JobPostId { get; set; }

    public Guid AppliedJobId { get; set; }

    public ApplicationStatus ApplicationStatus { get; set; }

    public DateTime ApplicationDate { get; set; }

    public virtual AppliedJob AppliedJob { get; set; } = null!;

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();

    public virtual JobPost JobPost { get; set; } = null!;
}
