using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; init; }

        protected List<IDomainEvent> _domainEvents { get; set; } = [];

        protected Entity(Guid id) => Id = id;

        protected Entity() { } //for reflection
    }
}
