using ErrorOr;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Queries
{
    public class LoginQueryHandler(
        IPasswordHasher _passwordHasher,
        IJwtTokenGenerator _jwtTokenGenerator,
        IUserRepository _userRepository) : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            return user is null || !user.IsCorrectPasswordHash(request.Password, _passwordHasher)
                ? AuthenticationErrors.InvalidCredentials
                : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
        }
    }
}
