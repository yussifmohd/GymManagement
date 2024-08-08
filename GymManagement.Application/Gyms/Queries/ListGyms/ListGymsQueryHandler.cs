using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.ListGyms
{
    public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
    {
        private readonly IGymRepository _gymsRepository;
        private readonly ISubscriptionRepository _subscriptionsRepository;

        public ListGymsQueryHandler(IGymRepository gymsRepository, ISubscriptionRepository subscriptionsRepository)
        {
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery request, CancellationToken cancellationToken)
        {
            if (!await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
            {
                return Error.NotFound(description: "Subscription not found");
            }

            return await _gymsRepository.ListBySubscriptionIdAsync(request.SubscriptionId);
        }
    }
}
