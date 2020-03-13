using Microsoft.AspNetCore.Mvc.Testing;
using PrimeTech.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace PrimeTech.Test
{
    public class AuthorControllerTest
    {
        private HttpClient httpClient;
        public AuthorControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            httpClient = appFactory.CreateClient();
        }
        [Fact]
        public async void Register_ReturnOk_IfSuccess()
        {
            var response = await httpClient.GetAsync("/api/auth/register");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
