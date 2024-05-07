using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkateAPI.Models;
using SkateAPI.Interfaces;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register request)
        {
            var result = await _userService.RegisterUserAsync(request);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Call the user service to handle login
            var result = await _userService.LoginUserAsync(request);
            if (result.Success)
            {
                // Return JWT token
                return Ok(new { token = result.Token });
            }
            return Unauthorized();
        }
    }
}
