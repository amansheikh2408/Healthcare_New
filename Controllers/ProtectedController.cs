using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly() => Ok(new { message = "Hello Admin" });

        [Authorize]
        [HttpGet("user")]
        public IActionResult AnyAuthenticated()
        {
            var email = User.Identity?.Name;
            return Ok(new { message = $"Hello {email}" });
        }
    }
}

