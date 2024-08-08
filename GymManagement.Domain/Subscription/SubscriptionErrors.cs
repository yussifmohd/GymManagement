using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Subscription
{
    public static class SubscriptionErrors
    {
        public static readonly Error CannotHaveMoreGymsThanTheSubscriptionAllows = Error.Validation(
            code: "Subscription.CannotHaveMoreGymsThanTheSubscriptionAllows",
            description: "A subscription cannot have more gyms than the subscription allows");
    }
}
