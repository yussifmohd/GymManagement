using GymManagement.Application.Rooms.Command.CreateRoom;
using GymManagement.Application.Rooms.Commands.DeleteRoom;
using GymManagement.Contracts.Rooms;
using GymManagement.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ApiController
    {
        private readonly ISender _mediator;

        public RoomsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request, Guid gymId)
        {
            var command = new CreateRoomCommand(gymId, request.Name);

            var createdRoomResults = await _mediator.Send(command);

            return createdRoomResults.Match(
                room => Created(
                    $"rooms/{room.Id}", // todo: add host
                    new RoomResponse(room.Id, room.Name)),
                Problem);
        }

        [HttpDelete("{roomId:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid gymId, Guid roomId)
        {
            var command = new DeleteRoomCommand(gymId, roomId);

            var deleteRoomResult = await _mediator.Send(command);

            return deleteRoomResult.Match(
                _ => NoContent(),
                Problem);
        }
    }
}
