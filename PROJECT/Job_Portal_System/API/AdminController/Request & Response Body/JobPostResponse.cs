using Domain.Enum;

namespace Job_Portal_System.API.AdminController.Request___Response_Body
{
    public class JobPostResponse
    {
        public Guid JobPostId { get; set; }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public decimal Salary { get; set; }


        public string Company { get; set; }

        public string Jobcategory { get; set; }

        public string Location { get; set; }

        public string JobType { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime DeadLine { get; set; }

        public string Status { get; set; }


    }
}
