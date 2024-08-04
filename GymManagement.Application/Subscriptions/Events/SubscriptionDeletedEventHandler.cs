using GymManagement.Domain.Admins.Events;
using GymManagment.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Events
{
    public class SubscriptionDeletedEventHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork) : INotificationHandler<SubscriptionDeletedEvent>
    {
        private readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(notification.subscriptionId);

            if (subscription is null)
            {
                throw new InvalidOperationException();
            }

            await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
            await _unitOfWork.CommitChangesAsync();
        }
    }
}
