using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Profiles.Queries.ListProfiles
{
    public class ListProfilesQueryHandler(IUserRepository _userRepository)
        : IRequestHandler<ListProfilesQuery, ErrorOr<ListProfilesResult>>
    {
        public async Task<ErrorOr<ListProfilesResult>> Handle(ListProfilesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                return Error.NotFound(description: "User not found");
            }

            return new ListProfilesResult(user.AdminId, user.ParticipantId, user.TrainerId);
        }
    }
}
