using Domain.Enum;

namespace Job_Portal_System.API.Authentication_Controller.Register
{
    public class Registerresponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Role? Roles { get; set; }
        public Status? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; } = null!;
    }
}
