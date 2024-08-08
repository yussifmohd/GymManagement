using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Command.CreateSubcription
{
    public class CreatSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork, IAdminRepository adminepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _adminRepository = adminepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId);

            if(admin is null)
            {
                return Error.NotFound(description: "Admin Not Found");
            }

            //Create Subscription
            var subscription = new Subscription(
                subscriptionType: request.SubscriptionType,
                adminId: request.AdminId);

            if(admin.SubscriptionId is not null)
            {
                return Error.Conflict(description: "Admin Already has an active subscription");
            }

            admin.SetSubscription(subscription);

            //Add In The DB
            await _subscriptionRepository.AddSubscriptionAsync(subscription);
            await _adminRepository.UpdateAsync(admin);
            await _unitOfWork.CommitChangesAsync();

            return subscription;
        }
    }
}
