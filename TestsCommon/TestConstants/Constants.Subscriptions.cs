using GymManagement.Domain.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsCommon.TestConstants
{
    public static partial class Constants
    {
        public static class Subscriptions
        {
            public static readonly SubscriptionType DefaultSubscription = SubscriptionType.Free;
            public static readonly Guid Id = Guid.NewGuid();
            public const int MaxSessionsFreeTier = 3;
            public const int MaxRoomsFreeTier = 1;
            public const int MaxGymsFreeTier = 1;
        }
    }
}
