namespace Job_Portal_System.API.AdminController
{
    public class JobTypeResponse
    {
        public Guid JobTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
