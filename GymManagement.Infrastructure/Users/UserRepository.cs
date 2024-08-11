using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Users;
using GymManagement.Infrastructure.Common.Presistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Users
{
    public class UserRepository(GymManagementDbContext _dbContext) : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public Task UpdateAsync(User user)
        {
            _dbContext.Update(user);

            return Task.CompletedTask;
        }
    }
}
