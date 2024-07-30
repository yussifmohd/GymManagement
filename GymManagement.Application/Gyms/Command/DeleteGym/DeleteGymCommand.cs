using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Command.DeleteGym
{
    public record DeleteGymCommand(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Deleted>>;
}