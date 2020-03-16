namespace PrimeTech.Infrastructure.Resources.Auth
{
    public class ResetPasswordResource
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
