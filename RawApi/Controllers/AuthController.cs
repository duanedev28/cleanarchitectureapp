using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RawApi.Application;
using RawApi.Domain.Entities;

namespace RawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid Credentials!");
            }

            return Ok(new { accessToken = token, expiresIn = 900000 });
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            await _authService.RegisterAsync(user.Name, user.PasswordHash);
            return Ok("User created successfully!");
        }
    }
}
