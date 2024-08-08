using GymManagement.Domain.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.TestConstants;

namespace TestsCommon.Subscriptions
{
    public static class SubscriptionFactory
    {
        public static Subscription CreateSubscription(SubscriptionType? subscriptionType = null, Guid? adminId = null, Guid? id = null)
        {
            return new Subscription(
                subscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscription,
                adminId ?? Constants.Admin.Id,
                id ?? Constants.Subscriptions.Id);
        }
    }
}
