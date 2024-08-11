using ErrorOr;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.Interfaces;
using GymManagement.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IPasswordHasher _passwordHasher,
        IUserRepository _userRepository,
        IUnitOfWork _unitOfWork) : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.ExistsByEmail(request.Email))
            {
                return Error.Conflict("Email Already Exists");
            }

            var HashPasswordResult = _passwordHasher.HashPassword(request.Password);

            if (HashPasswordResult.IsError)
            {
                return HashPasswordResult.Errors;
            }

            var user = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                HashPasswordResult.Value);

            await _userRepository.AddUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
