namespace Job_Portal_System.API.Authentication_Controller.Login
{
    public class Loginresponse
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
