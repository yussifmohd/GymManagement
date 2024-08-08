using GymManagement.Application.Subscriptions.Command.CreateSubcription;
using GymManagement.Application.Subscriptions.Command.DeleteSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscription.SubscriptionType;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ApiController
    {
        //private readonly IMediator _mediator;
        private readonly ISender _mediator; //We Use The Smaller Intefrace ISender Since this the one we use (Interface Segregation)

        public SubscriptionsController(ISender mediator)
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

            return CreateSubscription.Match(
                    subscription => CreatedAtAction(
                        nameof(GetSubscription),
                        new { subscriptionId = subscription.Id },
                        new SubscriptionResponse(
                            subscription.Id,
                            ToDto(subscription.SubscriptionType))),
                    Problem
                );
        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var GetSubscriptionResult = await _mediator.Send(query);

            return GetSubscriptionResult.Match(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    ToDto(subscription.SubscriptionType))),
                 Problem);
        }

        [HttpDelete("{subscriptionId:guid}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            var command = new DeleteSubscriptionCommand(subscriptionId);

            var createdSubscriptionResult = await _mediator.Send(command);

            return createdSubscriptionResult.Match(
                _ => NoContent(),
                Problem);
        }

        private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
        {
            return subscriptionType.Name switch
            {
                nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
                nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
                nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
