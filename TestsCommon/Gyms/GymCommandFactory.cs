using GymManagement.Application.Gyms.Command.CreateGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.TestConstants;

namespace TestsCommon.Gyms
{
    public static class GymCommandFactory
    {
        public static CreateGymCommand CreateCreateGymCommand(string name = Constants.Gym.Name, Guid? subscriptionId = null)
        {
            return new CreateGymCommand(name, SubscriptionId: subscriptionId ?? Constants.Subscriptions.Id);
        }
    }
}
