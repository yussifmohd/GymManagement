using GymManagment.Domain.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Application.Common.Interfaces
{
    public interface IGymRepository
    {
        Task<Gym> GetGymById(Guid id);
        Task<Gym> ExistsAsync(Guid id);
        Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
        Task AddGymAsync(Gym gym);
        Task UpdateGymAsync(Gym gym);
        Task RemoveGymAsync(Gym gym);
        Task RemoveRangeAsync(List<Gym> gyms);
    }
}
