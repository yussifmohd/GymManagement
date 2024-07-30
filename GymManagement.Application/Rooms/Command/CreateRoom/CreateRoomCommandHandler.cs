using ErrorOr;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Command.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        private readonly ISubscriptionRepository _subscriptionsRepository;
        private readonly IGymRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoomCommandHandler(ISubscriptionRepository subscriptionsRepository, IGymRepository gymsRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetGymByIdAsync(request.GymId);

            if (gym is null)
            {
                return Error.NotFound(description: "Gym not found");
            }

            var subscription = await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId);

            if (subscription is null)
            {
                return Error.Unexpected(description: "Subscription not found");
            }

            var room = new Room(
                name: request.RoomName,
                gymId: gym.Id,
                maxDailySessions: subscription.GetMaxDailySessions());

            var addGymResult = gym.AddRoom(room);

            if (addGymResult.IsError)
            {
                return addGymResult.Errors;
            }

            // Note: the room itself isn't stored in our database, out of the scope of the objective
            //Room Should Have A Seperate Table
            await _gymsRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return room;
        }
    }
}
