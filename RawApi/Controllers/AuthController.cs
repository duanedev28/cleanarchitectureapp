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

        [HttpPost(nameof(LoginAsync))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid Credentials!");
            }

            return Ok(new { accessToken = token, expiresIn = 900000 });
        }

        [HttpPost(nameof(SignupAsync))]
        public async Task<IActionResult> SignupAsync([FromBody] User user)
        {
            await _authService.RegisterAsync(user.Name, user.PasswordHash);
            return Ok("User created successfully!");
        }
    }
}
