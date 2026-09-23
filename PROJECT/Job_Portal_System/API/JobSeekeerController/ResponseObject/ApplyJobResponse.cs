namespace Job_Portal_System.API.JobSeekeerController.ResponseObject
{
    public class ApplyJobResponse
    {
        public string Message { get; set; } = null!;
        //public Guid JobApplicationId { get; set; }
        //public Guid AppliedJobId { get; set; }
        public Guid JobPostId { get; set; }
        public Guid JobSeekerProfileId { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
