using GymManagement.Domain.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task<bool> ExistsAsync(Guid id);
        Task<Subscription?> GetByAdminIdAsync(Guid adminId);
        Task<Subscription?> GetByIdAsync(Guid id);
        Task<List<Subscription>> ListAsync();
        Task RemoveSubscriptionAsync(Subscription subscription);
        Task UpdateAsync(Subscription subscription);
    }
}
