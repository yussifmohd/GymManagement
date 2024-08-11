using GymManagement.Application.Authentication.Commands.Register;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Authentication.Queries;
using GymManagement.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ISender _mediator) : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var authRes = await _mediator.Send(command);

            return authRes.Match(
                authResult => Ok(MapToAuthResponse(authResult)),
                Problem
                );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);

            var authRes = await _mediator.Send(query);

            if (authRes.IsError && authRes.FirstError == AuthenticationErrors.InvalidCredentials)
            {
                return Problem(
                    detail: authRes.FirstError.Description,
                    statusCode: StatusCodes.Status401Unauthorized
                    );
            }

            return authRes.Match(
                authRes => Ok(MapToAuthResponse(authRes)),
                Problem);
        }

        private AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
        }
    }
}
