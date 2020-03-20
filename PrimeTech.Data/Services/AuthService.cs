using Microsoft.AspNetCore.Identity;
using PrimeTech.Core.Models;
using PrimeTech.Core.Services;
using PrimeTech.Infrastructure.Consants;
using PrimeTech.Infrastructure.Resources.Auth;
using PrimeTech.Infrastructure.Response;
using PrimeTech.Infrastructure.Response.Auth;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using PrimeTech.Infrastructure.AppSettings;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PrimeTech.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly JwtSetting _jwtSetting;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            IOptions<JwtSetting> jwtSetting)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtSetting = jwtSetting.Value;
        }
        public async Task<GenericResponse<StatusResponse>> RegisterAsync(RegisterResource registerResource)
        {
            var user = await _userManager.FindByEmailAsync(registerResource.Email);

            if (user !=null)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account already exist!"
                };

            user = new ApplicationUser
            {
                FirstName = registerResource.FirstName,
                LastName = registerResource.LastName,
                PhoneNumber = registerResource.PhoneNumber,
                Email = registerResource.Email,
                UserName = registerResource.Email,
            };
            var result = await _userManager.CreateAsync(user, registerResource.Password);

            if (!result.Succeeded)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = string.Join(", ", result.Errors.Select(x => x.Description))
                };

            await SendEmailConfirmationLink(user);

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse { Status = $"Registration Successful, check your {user.Email} for link to confirm your email" }
            };
        }
        public async Task<GenericResponse<StatusResponse>> ConfirmAccountEmailAsync(ConfirmEmailResource confirmEmailResource)
        {
            var user = await _userManager.FindByIdAsync(confirmEmailResource.UserId);

            if (user == null)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account does not exist!"
                };

            if (await _userManager.IsEmailConfirmedAsync(user))
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The email is already verified"
                };

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailResource.Code);

            if (!result.Succeeded)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = string.Join(", ", result.Errors.Select(x => x.Description))
                };

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse
                {
                    Status = "The email has been successfully verified"
                }
            };
        }
        public async Task<GenericResponse<LoginResponse>> LoginAsync(LoginResource loginResource)
        {
            var user = await _userManager.FindByEmailAsync(loginResource.Email);

            if (user == null)
                return new GenericResponse<LoginResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account does not exist!"
                };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new GenericResponse<LoginResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The email is not yet verified"
                };

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginResource.Password);

            if (!isPasswordValid)
                return new GenericResponse<LoginResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account does not exist!"
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_jwtSetting.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddHours(4)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new GenericResponse<LoginResponse>
            {
                Succeeded = true,
                Data = new LoginResponse
                {
                    Token = tokenHandler.WriteToken(token)
                }
            };
        }

        public async Task<GenericResponse<StatusResponse>> ResendEmailConfirmationLinkAsync(EmailResource emailResource)
        {
            var user = await _userManager.FindByEmailAsync(emailResource.Email);

            if (user == null)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The account does not exist!"
                };

            if (await _userManager.IsEmailConfirmedAsync(user))
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The account email is already confirmed"
                };

            await SendEmailConfirmationLink(user);

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse
                {
                    Status = $"Please check your {user.Email} for link to confirm your email"
                }
            };
        }
        public async Task<GenericResponse<StatusResponse>> ForgetPasswordAsync(EmailResource emailResource)
        {
            var user = await _userManager.FindByEmailAsync(emailResource.Email);

            if (user == null)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account does not exists"
                };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account is inactive, try to confirm your email address"
                };

            await SendResetPassword(user);

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse
                {
                    Status = $"Check your {user.Email} for instruction to reset your password"
                }
            };
        }
        public async Task<GenericResponse<StatusResponse>> ResetPasswordAsync(ResetPasswordResource resource)
        {
            var user = await _userManager.FindByIdAsync(resource.UserId);

            if (user == null)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account does not exists"
                };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = "The user account is inactive, try to confirm your email address"
                };

            var result = await _userManager.ResetPasswordAsync(user, resource.Code, resource.Password);

            if (!result.Succeeded)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = string.Join(", ", result.Errors.Select(x => x.Description))
                };

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse
                {
                    Status = "The account password has been updated"
                }
            };
        }
        public async Task<GenericResponse<StatusResponse>> ChangePasswordAsync(string userId, ChangePasswordResource resource)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, resource.OldPassword, resource.NewPassword);

            if (!result.Succeeded)
                return new GenericResponse<StatusResponse>
                {
                    Succeeded = false,
                    ErrorMessage = string.Join(", ", result.Errors.Select(x => x.Description))
                };

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse
                {
                    Status = "The account password has been updated"
                }
            };
        }

        public async Task SendResetPassword(ApplicationUser user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var mailBody = await File.ReadAllTextAsync(EmailTemplate.ForgetPassword);
            var url = Link.ResetPassword.Replace("{userId}", user.Id).Replace("{code}", code);
            mailBody = mailBody.Replace("{{name}}", user.FirstName).Replace("{{URL}}", url);

            await _emailService.SendEmailAsync(mailBody, EmailSubject.ResetPassword, user.Email);
        }
        public async Task SendEmailConfirmationLink(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var mailBody = await File.ReadAllTextAsync(EmailTemplate.ConfirmEmail);
            var url = Link.EmailConfirmLink.Replace("{userId}", user.Id).Replace("{code}", code);
            mailBody = mailBody.Replace("{{name}}", user.FirstName).Replace("{{URL}}", url);

            await _emailService.SendEmailAsync(mailBody, EmailSubject.ConfirmEmail, user.Email);
        }
    }
}
