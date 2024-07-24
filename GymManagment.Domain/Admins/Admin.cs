using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace GymManagment.Domain.Admins
{
    public class Admin
    {
        public Admin(Guid userId, Guid? subscriptionId = null, Guid? id = null)
        {
            UserId = userId;
            SubscriptionId = subscriptionId;
            Id = id ?? Guid.NewGuid();
        }

        public Guid UserId { get; }
        public Guid? SubscriptionId { get; private set; } = null;
        public Guid Id { get; private set; }

        private Admin() { }


        public void DeleteSubscription(Guid subscriptionId)
        {
            SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);

            SubscriptionId = null;
        }

    }
}
