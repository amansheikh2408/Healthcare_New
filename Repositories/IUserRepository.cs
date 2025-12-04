using Healthcare.Models;

namespace Healthcare.Repositories
{
    public interface IUserRepository
    {

        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task SaveChangesAsync();
        Task<bool> AnyByEmailAsync(string email);
    }
}
