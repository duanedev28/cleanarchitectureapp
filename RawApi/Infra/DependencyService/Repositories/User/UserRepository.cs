using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RawApi.Domain;
using RawApi.Domain.Entities;

namespace RawApi.Infra
{
    public class UserRepository(AppDbContext context,
        IPasswordHasher<User> hasher) : IUserRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IPasswordHasher<User> _hasher = hasher;

        public async Task<User> AddUserAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Name == username))
            {
                throw new InvalidOperationException("User already exist");
            }

            var user = new User { Name = username };
            user.PasswordHash = _hasher.HashPassword(user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Name == username);
        }
    }
}
