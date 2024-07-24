using ErrorOr;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Subscription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Application.Subscriptions.Command.CreateSubcription
{
    public class CreatSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            //Create Subscription
            var Subscription = new Subscription(
                subscriptionType: request.SubscriptionType,
                adminId: request.AdminId);

            //Add In The DB
            await _subscriptionRepository.AddSubscriptionAsync(Subscription);
            await _unitOfWork.CommitChangesAsync();

            return Subscription;
        }
    }
}
