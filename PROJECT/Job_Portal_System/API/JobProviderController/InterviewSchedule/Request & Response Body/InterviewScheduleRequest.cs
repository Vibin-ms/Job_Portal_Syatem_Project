using System;

namespace Job_Portal_System.API.JobProviderController.InterviewSchedule.Request___Response_Body
{
    public class CreateInterviewRequest
    {
        public Guid JobApplicationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int InterviewMode { get; set; }
        public string MeetingLinkOrvenue { get; set; } = string.Empty;
    }

    public class UpdateInterviewRequest
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int InterviewMode { get; set; }
        public string MeetingLinkOrvenue { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
