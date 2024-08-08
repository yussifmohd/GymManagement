using GymManagement.Application.Subscriptions.Command.CreateSubcription;
using GymManagement.Domain.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.TestConstants;

namespace TestsCommon.Subscriptions
{
    public static class SubscriptionCommandFactory
    {
        public static CreateSubscriptionCommand CreateCreateSubscriptionCommand(
            SubscriptionType? subscriptionType = null,
            Guid? adminId = null
            )
        {
            return new CreateSubscriptionCommand(SubscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscription,
                AdminId: adminId ?? Constants.Admin.Id);
        }
    }
}
