using GymManagement.Domain.Admins.Events;
using GymManagement.Domain.Common;
using GymManagement.Domain.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace GymManagement.Domain.Admins
{
    public class Admin : Entity
    {
        public Admin(Guid userId, Guid? subscriptionId = null, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            SubscriptionId = subscriptionId;
        }

        public Guid UserId { get; }
        public Guid? SubscriptionId { get; private set; } = null;

        private Admin() { }

        public void SetSubscription(Subscription.Subscription subscription)
        {
            SubscriptionId.HasValue.Throw().IfTrue();
            SubscriptionId = subscription.Id;
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);

            SubscriptionId = null;

            _domainEvents.Add(new SubscriptionDeletedEvent(subscriptionId));
        }
    }
}
