namespace Job_Portal_System.API.JobSeekeerController.Request___Response_Body.Jobseekerprofile
{
    public class Createrequest
    {
        
        public string? About { get; set; }
        public string? ResumeUrl { get; set; }

      
        public Guid SkillId { get; set; }
        public Guid QualificationId { get; set; }
        public Guid ExperienceId { get; set; }
        public Guid LocationId { get; set; }
    }
}
