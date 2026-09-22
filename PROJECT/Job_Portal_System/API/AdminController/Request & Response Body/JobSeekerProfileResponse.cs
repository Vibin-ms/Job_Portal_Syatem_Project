namespace Job_Portal_System.API.AdminController.Request___Response_Body
{
    public class JobSeekerProfileResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string? About { get; set; }
        public string? ResumeUrl { get; set; }

        public string? SkillName { get; set; }
        public string? QualificationName { get; set; }
        public string? ExperienceName { get; set; }
        public string? LocationName { get; set; }
    }
}
