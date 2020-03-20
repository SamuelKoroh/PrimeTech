using Microsoft.AspNetCore.Mvc;
using PrimeTech.Api.Extensions;
using PrimeTech.Core.Services;
using PrimeTech.Infrastructure.Resources.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrimeTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterResource registerResource)
        {
            var result = await _authService.RegisterAsync(registerResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }


        [HttpPost("verify-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailResource confirmEmailResource)
        {
            var result = await _authService.ConfirmAccountEmailAsync(confirmEmailResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("resend-email-confirmation")]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] EmailResource email)
        {
            var result = await _authService.ResendEmailConfirmationLinkAsync(email);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResource loginResource)
        {
            var result = await _authService.LoginAsync(loginResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailResource emailResource)
        {
            var result = await _authService.ForgetPasswordAsync(emailResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource resetPasswordResource)
        {
            var result = await _authService.ResetPasswordAsync(resetPasswordResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResource changePasswordResource)
        {
            var result = await _authService.ChangePasswordAsync(User.GetUserId(), changePasswordResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}