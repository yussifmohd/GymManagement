using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Gyms;
using GymManagment.Infrastructure.Common.Presistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Gyms.Presistance
{
    public class GymRepository : IGymRepository
    {
        private readonly GymManagementDbContext _dbContext;

        public GymRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddGymAsync(Gym gym)
        {
            await _dbContext.AddAsync(gym);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
        }

        public async Task<Gym?> GetGymById(Guid id)
        {
            return await _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
        }
        
        public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await _dbContext.Gyms
                .Where(gym => gym.SubscriptionId == subscriptionId)
                .ToListAsync();
        }

        public Task RemoveGymAsync(Gym gym)
        {
            _dbContext.Remove(gym);

            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Gym> gyms)
        {
            _dbContext.RemoveRange(gyms);

            return Task.CompletedTask;
        }

        public Task UpdateGymAsync(Gym gym)
        {
            _dbContext.Update(gym);

            return Task.CompletedTask;
        }
    }
}
