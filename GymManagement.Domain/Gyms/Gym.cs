using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Domain.Gyms
{
    public class Gym
    {
        private readonly int _maxRooms;

        public Guid Id { get; }
        private readonly List<Guid> _roomIds = [];
        private readonly List<Guid> _tranierId = [];

        public Gym(string name, int maxRooms, Guid subscriptionId, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            _maxRooms = maxRooms;
            SubscriptionId = subscriptionId;
        }

        public string Name { get; set; } = null!; //should be assigned only during object creation
        public Guid SubscriptionId { get; init; }

    }
}
