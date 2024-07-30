using ErrorOr;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Command.CreateGym
{
    public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGymCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

            if(subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var gym = new Gym(
                name: request.Name,
                maxRooms: subscription.GetMaxRooms(),
                subscriptionId: subscription.Id);

            var addGymResult = subscription.AddGym(gym);

            if (addGymResult.IsError)
            {
                return addGymResult.Errors;
            }

            await _subscriptionRepository.UpdateAsync(subscription);
            await _gymRepository.AddGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return gym;
        }
    }
}
