namespace Job_Portal_System.API.JobSeekeerController.ResponseObject
{
    public class SaveJobResponse
    {
        
        public string Message { get; set; } = null!;

        public Guid SavedJobId { get; set; }

        //public Guid JobPostId { get; set; }

        public Guid JobSeekerProfileId { get; set; }

        public DateTime SavedDate { get; set; }
    }
}
