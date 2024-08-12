using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Profiles.Queries.ListProfiles
{
    public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;
    
}
