namespace Job_Portal_System.API.JobSeekeerController.ResponseObject
{
    public class GetInterviewScheduleResponse
    {
        //public Guid InterviewScheduleId { get; set; }

        //public Guid JobApplicationId { get; set; }

        //public Guid JobProviderId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime ScheduledStartTime { get; set; }

        public DateTime ScheduledEndTime { get; set; }

        //public int InterviewMode { get; set; }

        public string MeetingLinkOrvenue { get; set; } = null!;

        //public int Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
