using GymManagement.Application.Profiles.Commands.CreateAdminProfile;
using GymManagement.Application.Profiles.Queries.ListProfiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(ISender _mediator) : ApiController
    {
        [HttpPost("admin")]
        [Authorize]
        public async Task<IActionResult> CreateAdminProfile(Guid userId)
        {
            var command = new CreateAdminProfileCommand(userId);

            var createdProfile = await _mediator.Send(command);

            return createdProfile.Match(
                id => Ok(new { id }),
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> ListProfiles(Guid userId)
        {
            var listProfileQuery = new ListProfilesQuery(userId);

            var listProfiles = await _mediator.Send(listProfileQuery);

            return listProfiles.Match(
                profiles => Ok(new { profiles.AdminId, profiles.ParticipantId, profiles.TrainerId }),
                Problem);
        }

    }
}
