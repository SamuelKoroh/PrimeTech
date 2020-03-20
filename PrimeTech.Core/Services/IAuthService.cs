using PrimeTech.Infrastructure.Resources.Auth;
using PrimeTech.Infrastructure.Response;
using PrimeTech.Infrastructure.Response.Auth;
using System.Threading.Tasks;

namespace PrimeTech.Core.Services
{
    public interface IAuthService
    {
        Task<GenericResponse<StatusResponse>> RegisterAsync(RegisterResource registerResource);
        Task<GenericResponse<StatusResponse>> ConfirmAccountEmailAsync(ConfirmEmailResource confirmEmailResource);
        Task<GenericResponse<LoginResponse>> LoginAsync(LoginResource loginResource);
        Task<GenericResponse<StatusResponse>> ResendEmailConfirmationLinkAsync(EmailResource emailResource);
        Task<GenericResponse<StatusResponse>> ForgetPasswordAsync(EmailResource emailResource);
        Task<GenericResponse<StatusResponse>> ResetPasswordAsync(ResetPasswordResource resetPasswordResource);
        Task<GenericResponse<StatusResponse>> ChangePasswordAsync(string userId, ChangePasswordResource emailResource);
    }
}
