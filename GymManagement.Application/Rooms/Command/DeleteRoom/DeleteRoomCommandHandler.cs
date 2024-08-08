using ErrorOr;
using GymManagement.Application.Rooms.Commands.DeleteRoom;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Command.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
    {
        private readonly IGymRepository _gymsRepository;
        private readonly ISubscriptionRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomCommandHandler(IGymRepository gymsRepository, ISubscriptionRepository subscriptionsRepository, IUnitOfWork unitOfWork)
        {
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetGymByIdAsync(request.GymId);

            if (gym is null)
            {
                return Error.NotFound(description: "Gym not found");
            }

            if (!gym.HasRoom(request.RoomId))
            {
                return Error.NotFound(description: "Room not found");
            }

            gym.RemoveRoom(request.RoomId);

            await _gymsRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;

        }
    }
}
