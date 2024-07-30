using ErrorOr;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.GetGym
{
    public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
    {
        private readonly IGymRepository _gymsRepository;
        private readonly ISubscriptionRepository _subscriptionsRepository;

        public GetGymQueryHandler(IGymRepository gymsRepository, ISubscriptionRepository subscriptionsRepository)
        {
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
        {
            if (!await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
            {
                return Error.NotFound(description: "Subscription Not Found");
            }

            if(await _gymsRepository.GetGymByIdAsync(request.GymId) is not Gym gym)
            {
                return Error.NotFound(description: "Gym Not Found");
            }

            return gym;
        }
    }
}
