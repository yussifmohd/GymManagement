using ErrorOr;
using GymManagment.Domain.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Command.CreateRoom
{
    public record CreateRoomCommand(Guid GymId, string RoomName) : IRequest<ErrorOr<Room>>;
}
