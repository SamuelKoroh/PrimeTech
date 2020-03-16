namespace PrimeTech.Infrastructure.Resources.Auth
{
    public class ChangePasswordResource
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
