using GymManagment.Application.Subscriptions.Command.CreateSubcription;
using GymManagment.Application.Subscriptions.Queries.GetSubscription;
using GymManagment.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagment.Domain.Subscription.SubscriptionType;

namespace GymManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        //private readonly IMediator _mediator;
        private readonly ISender _mediator; //We Use The Smaller Intefrace ISender Since this the one we use (Interface Segregation)

        public SubscriptionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubsription(CreateSubscriptionRequest request)
        {
            if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subsriptionType))
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid Subscription Type");
            }
            var command = new CreateSubscriptionCommand(subsriptionType, request.AdminId);
            var CreateSubscription = await _mediator.Send(command);

            return CreateSubscription.MatchFirst(
                subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
                error => Problem()
                );
        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var GetSubscriptionResult = await _mediator.Send(query);

            return GetSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name)
                    )),
                error => Problem()
                );
        }

    }
}
