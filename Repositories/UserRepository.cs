using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        public Task<User?> GetByUsernameAsync(string username)
        {
            return db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
