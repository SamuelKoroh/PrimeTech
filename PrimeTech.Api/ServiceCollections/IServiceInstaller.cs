using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeTech.Api.ServiceCollections
{
    public interface IServiceInstaller
    {
        void configureService(IServiceCollection services, IConfiguration configuration);
    }
}
