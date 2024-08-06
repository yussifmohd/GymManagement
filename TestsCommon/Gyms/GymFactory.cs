using GymManagment.Domain.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.TestConstants;

namespace TestsCommon.Gyms
{
    public static class GymFactory
    {
        public static Gym CreateGym(
            string name = Constants.Gym.Name,
            int maxRooms = Constants.Subscriptions.MaxRoomsFreeTier,
            Guid? id = null)
        {
            return new Gym(
                name,
                maxRooms,
                subscriptionId: Constants.Subscriptions.Id,
                id: id ?? Guid.NewGuid());
        }
    }
}
