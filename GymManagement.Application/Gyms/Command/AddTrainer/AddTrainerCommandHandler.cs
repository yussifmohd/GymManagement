using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Command.AddTrainer
{
    public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
    {
        private readonly IGymRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTrainerCommandHandler(IGymRepository gymsRepository, IUnitOfWork unitOfWork)
        {
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            Gym? gym = await _gymsRepository.GetGymByIdAsync(request.GymId);

            if(gym is null)
            {
                return Error.NotFound(description: "Gym Not Found");
            }

            var addTrainer = gym.AddTrainer(request.TrainerId);

            if (addTrainer.IsError)
            {
                return addTrainer.Errors;
            }

            await _gymsRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}
