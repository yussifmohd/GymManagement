using GymManagement.Domain.Common;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Admins;
using GymManagment.Domain.Gyms;
using GymManagment.Domain.Subscription;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Infrastructure.Common.Presistence
{
    public class GymManagementDbContext(
        DbContextOptions options,
        IHttpContextAccessor httpContextAccessor,
        IPublisher _publisher) : DbContext(options), IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Admin> Admins {  get; set; }
        public DbSet<Gym> Gyms { get; set; }

        public async Task CommitChangesAsync()
        {
            // get hold of all the domain events
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(entry => entry.Entity.PopDomainEvents())
                .SelectMany(x => x)
                .ToList();

            // store them in the http context for later if user is waiting online
            if (IsUserWaitingOnline())
            {
                AddDomainEventsToOfflineProcessingQueue(domainEvents);
            }
            else
            {
                await PublishDomainEvents(_publisher, domainEvents);
            }

            await SaveChangesAsync();
        }

        private async Task PublishDomainEvents(IPublisher publisher, List<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }

        private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
        {
            // fetch queue from http context or create a new queue if it doesn't exist
            var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomain
                ? existingDomain
                : new Queue<IDomainEvent>();

            // add the domain events to the end of the queue
            domainEvents.ForEach(domainEventsQueue.Enqueue);

            // store the queue in the http context
            _httpContextAccessor.HttpContext.Items["DomainEventsQueue"] = domainEventsQueue;
        }

        private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
