namespace Job_Portal_System.API.AdminController.Request___Response_Body
{
    public class CompanyResponse
    {
        public Guid CompanyId { get; set; }

        public string Industry { get; set; }

        public string Location { get; set; }

        public string ComapnyName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string Status { get; set; }

    }
}
