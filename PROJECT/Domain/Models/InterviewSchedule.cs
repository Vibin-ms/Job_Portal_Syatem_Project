using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class InterviewSchedule
{
    public Guid InterviewScheduleId { get; set; }

    public Guid JobApplicationId { get; set; }

    public Guid JobProviderId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime ScheduledStartTime { get; set; }

    public DateTime ScheduledEndTime { get; set; }

    public int InterviewMode { get; set; }

    public string MeetingLinkOrvenue { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual JobApplication JobApplication { get; set; } = null!;

    public virtual JobProvider JobProvider { get; set; } = null!;
}
