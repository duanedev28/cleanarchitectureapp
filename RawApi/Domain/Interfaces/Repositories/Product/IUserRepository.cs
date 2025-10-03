using RawApi.Domain.Entities;

namespace RawApi.Domain
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(string username, string password);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
