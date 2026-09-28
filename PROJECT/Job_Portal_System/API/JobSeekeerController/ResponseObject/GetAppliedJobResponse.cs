namespace Job_Portal_System.API.JobSeekeerController.ResponseObject
{
    public class GetAppliedJobResponse
    {
        //public Guid JobApplicationId { get; set; }

        //public Guid AppliedJobId { get; set; }

        public Guid JobPostId { get; set; }

        public string JobTitle { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public int ApplicationStatus { get; set; }

        public DateTime ApplicationDate { get; set; }

        public DateTime AppliedDate { get; set; }
    }
}
