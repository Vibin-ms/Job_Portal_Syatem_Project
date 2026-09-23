namespace Job_Portal_System.API.JobSeekeerController.ResponseObject
{
    public class SearchJobResponse
    {
        public Guid JobPostId { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Salary { get; set; }

        public string Company { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string JobType { get; set; } = null!;
        public string Location { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
