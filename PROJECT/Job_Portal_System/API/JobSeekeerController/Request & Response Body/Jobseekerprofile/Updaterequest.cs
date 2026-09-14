namespace Job_Portal_System.API.JobSeekeerController.Request___Response_Body.Jobseekerprofile
{
    public class Updaterequest
    {
        // JobSeekerProfile
        public string? About { get; set; }
        public string? ResumeUrl { get; set; }

        // SystemUser
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        // Existing Admin Master Data
        public Guid SkillId { get; set; }
        public Guid QualificationId { get; set; }
        public Guid ExperienceId { get; set; }
        public Guid LocationId { get; set; }
    }
}
