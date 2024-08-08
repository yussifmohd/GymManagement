using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Rooms
{
    public class Room
    {
        public Room(string name, Guid gymId, int maxDailySessions, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            GymId = gymId;
            MaxDailySessions = maxDailySessions;
        }

        public Guid Id { get; }
        public string Name { get; } = null!;

        public Guid GymId { get; }
        public int MaxDailySessions { get; }
    }
}
