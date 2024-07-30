using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Command.AddTrainer
{
    public record AddTrainerCommand(Guid GymId, Guid TrainerId)
        : IRequest<ErrorOr<Success>>;
}
