namespace Job_Portal_System.API.JobSeekeerController.ResponseObject
{
    public class GetSavedJobResponse
    {
        public Guid SavedJobId { get; set; }

        //public Guid JobPostId { get; set; }

        //public Guid JobSeekerProfileId { get; set; }

        public string JobTitle { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public DateTime SavedDate { get; set; }
    }
}
