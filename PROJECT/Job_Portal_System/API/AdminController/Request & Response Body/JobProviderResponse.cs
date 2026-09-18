using Domain.Enum;

namespace Job_Portal_System.API.AdminController.Request___Response_Body
{
    public class JobProviderResponse
    {
        public Guid JobProviderId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Roles { get; set; }

    }
}
