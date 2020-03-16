using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeTech.Core.Models;
using PrimeTech.Data;
using PrimeTech.Infrastructure.AppSettings;

namespace PrimeTech.Api.ServiceCollections
{
    public class ConfigureDatabase : IServiceInstaller
    {
        public void configureService(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DATABASE_URL");

            services.AddDbContext<PrimeTechDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>( config => {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<PrimeTechDbContext>()
                .AddDefaultTokenProviders();

            services.AddHangfire(c => c.UsePostgreSqlStorage(connectionString));

            services.Configure<JwtSetting>(configuration.GetSection("JwtSetting"));
            services.Configure<SendGridSetting>(configuration.GetSection("SendGridSetting"));

        }
    }
}
