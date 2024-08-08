using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Command.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubscriptionCommandHandler(IAdminRepository adminRepository, ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _subscriptionRepository = subscriptionRepository;
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

            if(subscription is null)
            {
                return Error.NotFound(description: "Subscription Not Found");
            }

            var admin = await _adminRepository.GetByIdAsync(subscription.AdminId);

            if(admin is null)
            {
                return Error.Unexpected(description: "Admin Not Found");
            }

            admin.DeleteSubscription(request.SubscriptionId);

            //var gymsToDelete = await _gymRepository.ListBySubscriptionIdAsync(request.SubscriptionId);

            await _adminRepository.UpdateAsync(admin);
            //await _gymRepository.RemoveRangeAsync(gymsToDelete);
            //await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}
