using ErrorOr;
using GymManagement.Domain.Subscription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Command.CreateSubcription
{
    public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;
}
