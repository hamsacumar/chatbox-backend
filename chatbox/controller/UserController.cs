using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Accessible to any authenticated user (token required)
        [Authorize]
        [HttpGet("user-data")]
        public IActionResult GetUserData()
        {
            return Ok("Accessible to all authenticated users");
        }

        // Only accessible to users with the "Admin" role
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-data")]
        public IActionResult GetAdminData()
        {
            return Ok("Only accessible to Admin");
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var email = User.Identity?.Name;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new { email, role });
        }

    }
}
