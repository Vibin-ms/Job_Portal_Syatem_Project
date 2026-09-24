using System;

namespace Domain.Services.InterviewSchedules.Dto
{
    public class InterviewScheduleDto
    {
        public Guid InterviewScheduleId { get; set; }
        public Guid JobApplicationId { get; set; }
        public string JobPostTitle { get; set; } = string.Empty;
        public string CandidateName { get; set; } = string.Empty;
        public Guid JobProviderId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int InterviewMode { get; set; }
        public string MeetingLinkOrvenue { get; set; } = string.Empty;
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CreateInterviewScheduleDto
    {
        public Guid JobApplicationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int InterviewMode { get; set; }
        public string MeetingLinkOrvenue { get; set; } = string.Empty;
    }

    public class UpdateInterviewScheduleDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int InterviewMode { get; set; }
        public string MeetingLinkOrvenue { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
