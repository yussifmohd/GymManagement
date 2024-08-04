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

        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();

            _domainEvents.Clear();

            return copy;
        }

        protected Entity(Guid id) => Id = id;

        protected Entity() { } //for reflection
    }
}
