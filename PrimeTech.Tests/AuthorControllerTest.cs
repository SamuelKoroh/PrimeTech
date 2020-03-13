using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace PrimeTech.Test
{
    public class AuthorControllerTest : InitialSetup
    {
        [Fact]
        public async void Register_ReturnOk_IfSuccess()
        {
            var response = await httpClient.GetAsync("/api/auth/register");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
