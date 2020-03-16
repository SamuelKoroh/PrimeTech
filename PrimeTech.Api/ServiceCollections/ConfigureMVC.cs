using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeTech.Infrastructure.Response.Error;
using System.Linq;

namespace PrimeTech.Api.ServiceCollections
{
    public class ConfigureMVC : IServiceInstaller
    {
        public void configureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddControllers()
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0)
                     .Select(e => new ErrorModel
                     {
                         FieldName = e.Key,
                         Message = e.Value.Errors.Select(e => e.ErrorMessage).ToList()
                     });

                    var errorResponse = new ErrorResponse()
                    {
                        Status = "error",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }
    }
}
