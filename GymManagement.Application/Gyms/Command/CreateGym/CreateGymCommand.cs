using ErrorOr;
using GymManagement.Application.Common.Authorization;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Command.CreateGym
{
    //[Authorize(Permissions = "gyms:create,gyms:update")]
    [Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Admin", Permissions = "gyms:create")]
    public record CreateGymCommand(string Name, Guid SubscriptionId)
        : IRequest<ErrorOr<Gym>>;
}
