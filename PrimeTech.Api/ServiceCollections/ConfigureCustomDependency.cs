using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeTech.Core.Services;
using PrimeTech.Data.Services;

namespace PrimeTech.Api.ServiceCollections
{
    public class ConfigureCustomDependency: IServiceInstaller
    {
        public void configureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
