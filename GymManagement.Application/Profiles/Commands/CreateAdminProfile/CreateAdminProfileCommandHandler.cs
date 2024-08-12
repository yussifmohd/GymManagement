using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using MediatR;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error = ErrorOr.Error;

namespace GymManagement.Application.Profiles.Commands.CreateAdminProfile
{
    public class CreateAdminProfileCommandHandler(
        IUserRepository _userRepository,
        IAdminRepository _adminRepository,
        IUnitOfWork _unitOfWork,
        ICurrentUserProvider _currentUserProvider)
        : IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (currentUser.Id != request.UserId)
            {
                return Error.Unauthorized(description: "User is forbidden from taking this action");
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                return Error.NotFound("User Not Found");
            }

            var createdAdminProfile = user.CreateAdminProfile();
            var admin = new Admin(userId: user.Id, id: createdAdminProfile.Value);

            await _userRepository.UpdateAsync(user);
            await _adminRepository.AddAdminAsync(admin);
            await _unitOfWork.CommitChangesAsync();

            return createdAdminProfile;
        }
    }
}
