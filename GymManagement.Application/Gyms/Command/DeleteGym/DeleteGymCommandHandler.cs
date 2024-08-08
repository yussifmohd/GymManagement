using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Command.DeleteGym
{
    public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGymCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
        {
            var gym = await _gymRepository.GetGymByIdAsync(request.GymId);

            if (gym is null)
            {
                return Error.NotFound(description: "Gym Not Found");
            }

            var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription Not Found");
            }

            if (!subscription.HasGym(request.GymId))
            {
                return Error.Unexpected(description: "Gym Not Found For This Subscription");
            }

            subscription.RemoveGym(request.GymId);

            await _subscriptionRepository.UpdateAsync(subscription);
            await _gymRepository.RemoveGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}
