using ErrorOr;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Subscription;
using GymManagment.Infrastructure.Common.Presistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GymManagmentDbContext _dbContext;

        public SubscriptionRepository(GymManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _dbContext.Subscriptions.AddAsync(subscription);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Subscriptions.AsNoTracking().AnyAsync(subscription => subscription.Id == id);
        }

        public async Task<Subscription?> GetByAdminIdAsync(Guid adminId)
        {
            return await _dbContext.Subscriptions.AsNoTracking().FirstOrDefaultAsync(s => s.AdminId == adminId);
        }

        public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            return await _dbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionId);
        }

        public async Task<List<Subscription>> ListAsync()
        {
            return await _dbContext.Subscriptions.ToListAsync();
        }

        public Task RemoveSubscriptionAsync(Subscription subscription)
        {
            _dbContext.Subscriptions.Remove(subscription);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Subscription subscription)
        {
            _dbContext.Update(subscription);
            return Task.CompletedTask;
        }
    }
}
