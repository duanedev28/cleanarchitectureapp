using Microsoft.AspNetCore.Identity;
using RawApi.Application.Interfaces;
using RawApi.Domain;
using RawApi.Domain.Entities;

namespace RawApi.Application
{
    public class AuthService(IUserRepository userRepository,
        IPasswordHasher<User> hasher,
        IJwtService jwtService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _hasher = hasher;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<string> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("username and password should not be empty");
            };

            var user = await _userRepository.GetUserByUsernameAsync(username) ?? 
                throw new UnauthorizedAccessException("User not found");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result != PasswordVerificationResult.Success && 
                    result != PasswordVerificationResult.SuccessRehashNeeded)
            {
                throw new Exception("Invalid credentials");
            }

            return _jwtService.GenerateToken(user.Id, user.Name, user.Role);
        }

        public async Task RegisterAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("username and password should not be empty");
            };

            await _userRepository.AddUserAsync(username, password);
        }
    }
}
