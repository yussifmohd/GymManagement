using ErrorOr;
using GymManagment.Domain.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace GymManagment.Domain.Gyms
{
    public class Gym
    {
        private readonly int _maxRooms;

        public Guid Id { get; }
        private readonly List<Guid> _roomIds = [];
        private readonly List<Guid> _trainerIds = [];

        public Gym(string name, int maxRooms, Guid subscriptionId, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            _maxRooms = maxRooms;
            SubscriptionId = subscriptionId;
        }

        public string Name { get; set; } = null!; //should be assigned only during object creation
        public Guid SubscriptionId { get; init; }

        public ErrorOr<Success> AddRoom(Room room)
        {
            _roomIds.Throw().IfContains(room.Id);

            if (_roomIds.Count >= _maxRooms)
            {
                return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
            }

            _roomIds.Add(room.Id);

            return Result.Success;
        }

        public bool HasRoom(Guid roomId)
        {
            return _roomIds.Contains(roomId);
        }

        public ErrorOr<Success> AddTrainer(Guid trainerId)
        {
            if (_trainerIds.Contains(trainerId))
            {
                return Error.Conflict(description: "Trainer already added to gym");
            }

            _trainerIds.Add(trainerId);
            return Result.Success;
        }

        public bool HasTrainer(Guid trainerId)
        {
            return _trainerIds.Contains(trainerId);
        }

        public void RemoveRoom(Guid roomId)
        {
            _roomIds.Remove(roomId);
        }

        private Gym() { }

    }
}
