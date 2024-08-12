using GymManagement.Application.Gyms.Command.AddTrainer;
using GymManagement.Application.Gyms.Command.CreateGym;
using GymManagement.Application.Gyms.Command.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
using GymManagement.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymsController(ISender _mediator) : ApiController
    {
        [HttpPost("{subscriptionId}")]
        public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
        {
            var command = new CreateGymCommand(request.Name, subscriptionId);

            var createdGymResult = await _mediator.Send(command);

            return createdGymResult.Match(
                gym => CreatedAtAction(
                    nameof(GetGym),
                    new { subscriptionId, GymId = gym.Id },
                    new GymResponse(gym.Id, gym.Name)),
                Problem
                );
        }

        [HttpDelete("{gymId:guid}")]
        public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
        {
            var command = new DeleteGymCommand(subscriptionId, gymId);

            var deletedGymResult = await _mediator.Send(command);

            return deletedGymResult.Match(
                _ => NoContent(),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> ListGyms(Guid subscriptionId)
        {
            var command = new ListGymsQuery(subscriptionId);

            var listGymsResult = await _mediator.Send(command);

            return listGymsResult.Match(
                gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
                Problem);
        }

        [HttpGet("{gymId:guid}")]
        public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
        {
            var command = new GetGymQuery(subscriptionId, gymId);

            var getGymResult = await _mediator.Send(command);

            return getGymResult.Match(
                gym => Ok(new GymResponse(gym.Id, gym.Name)),
                Problem);
        }

        [HttpPost("{gymId:guid}/trainers")]
        public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid gymId)
        {
            var command = new AddTrainerCommand(gymId, request.TrainerId);

            var addTrainerResult = await _mediator.Send(command);

            return addTrainerResult.Match(
                success => Ok(),
                Problem);
        }
    }
}
