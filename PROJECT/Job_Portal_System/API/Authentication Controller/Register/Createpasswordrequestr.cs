namespace Job_Portal_System.API.Authentication_Controller.Register
{
    public class Createpasswordrequestr
    {
        public Guid UserId { get; set; }

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
