//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using PrimeTech.Api;
//using PrimeTech.Data;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Linq;

//namespace PrimeTech.Test
//{
//    public class InitialSetup : IDisposable
//    {
//        protected readonly HttpClient httpClient;
//        private readonly IServiceProvider serviceProvider;
//        public InitialSetup()
//        {
//            var appFactory = new WebApplicationFactory<Startup>()
//                .WithWebHostBuilder(builder => {
//                    builder.ConfigureServices(services =>
//                    {
//                        var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(PrimeTechDbContext));
//                        services.Remove(serviceDescriptor);
//                        services.AddDbContext<PrimeTechDbContext>(opt => opt.UseInMemoryDatabase("TestDB"));
//                    });
//                });
//            httpClient = appFactory.CreateClient();
//            serviceProvider = appFactory.Services;
//        }

//        public void Dispose()
//        {
//            using var scope = serviceProvider.CreateScope();
//            var context = scope.ServiceProvider.GetService<PrimeTechDbContext>();
//            context.Database.EnsureDeleted();
//        }
//    }
//}
