﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Contracts.Subscriptions
{
    public record CreateSubscriptionRequest(SubscriptionType SubscriptionType, Guid AdminId);

}
