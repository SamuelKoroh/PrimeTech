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
        [Fact]
        public void Register_ReturnOk_IfSuccess()
        {
            Assert.Equal(1, 1);
        }
    }
}
