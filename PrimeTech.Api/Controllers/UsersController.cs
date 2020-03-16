using Microsoft.AspNetCore.Mvc;

namespace PrimeTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("register")]
        public IActionResult Register()
        {
            return Ok();
        }
    }
}