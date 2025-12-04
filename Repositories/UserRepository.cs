using Healthcare.Data;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Repositories
{
    public class UserRepository: IUserRepository
    {

        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public async Task<User?> GetByEmailAsync(string email)
            => await _db.Users.SingleOrDefaultAsync(u => u.Email == email);

        public async Task<User> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task<bool> AnyByEmailAsync(string email)
            => await _db.Users.AnyAsync(u => u.Email == email);
    }
}

