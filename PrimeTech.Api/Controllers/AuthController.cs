using Microsoft.AspNetCore.Mvc;
using PrimeTech.Core.Services;
using PrimeTech.Infrastructure.Resources.Auth;
using System.Threading.Tasks;

namespace PrimeTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterResource registerResource)
        {
            var result = await _authService.RegisterAsync(registerResource);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}