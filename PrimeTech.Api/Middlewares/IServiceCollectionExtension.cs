using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeTech.Api.ServiceCollections;
using System;
using System.Linq;

namespace PrimeTech.Api.Middlewares
{
    public static class IServiceCollectionExtension
    {
        public static void AddRequiredServices(this IServiceCollection services, IConfiguration configuration)
        {
            typeof(Startup).Assembly.GetExportedTypes()
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList()
                .ForEach(service => service.configureService(services, configuration));
        }
    }
}
