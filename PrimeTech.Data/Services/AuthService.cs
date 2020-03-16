using AutoMapper;
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

namespace PrimeTech.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<GenericResponse<StatusResponse>> RegisterAsync(RegisterResource registerResource)
        {
            var user = new ApplicationUser 
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

            BackgroundJob.Enqueue(() => SendEmailConfirmationLink(user));

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse { Status = $"Registration Successful, check your {user.Email} for link to confirm your email" }
            };
        }
        public Task<GenericResponse<StatusResponse>> ConfirmAccountEmailAsync(ConfirmEmailResource confirmEmailResource)
        {
            throw new NotImplementedException();
        }
        public Task<GenericResponse<LoginResponse>> LoginAsync(LoginResource loginResource)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<StatusResponse>> ResendEmailConfirmationLinkAsync(EmailResource emailResource)
        {
            var user = await _userManager.FindByEmailAsync(emailResource.Email);

            if(user == null || await _userManager.IsEmailConfirmedAsync(user))
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

            BackgroundJob.Enqueue(() => SendEmailConfirmationLink(user));

            return new GenericResponse<StatusResponse>
            {
                Succeeded = true,
                Data = new StatusResponse { Status = $"Please check your {user.Email} for link to confirm your email" }
            };
        }
        public Task<GenericResponse<StatusResponse>> ForgetPasswordAsync(EmailResource emailResource)
        {
            throw new NotImplementedException();
        }
        public Task<GenericResponse<StatusResponse>> ResetPasswordAsync(ResetPasswordResource resetPasswordResource)
        {
            throw new NotImplementedException();
        }
        public Task<GenericResponse<StatusResponse>> ChangePasswordAsync(ChangePasswordResource emailResource)
        {
            throw new NotImplementedException();
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
