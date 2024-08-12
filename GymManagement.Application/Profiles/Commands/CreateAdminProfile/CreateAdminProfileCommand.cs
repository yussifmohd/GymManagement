using ErrorOr;
using MediatR;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Profiles.Commands.CreateAdminProfile
{
    public record CreateAdminProfileCommand(Guid UserId) : IRequest<ErrorOr<Guid>>;
}
