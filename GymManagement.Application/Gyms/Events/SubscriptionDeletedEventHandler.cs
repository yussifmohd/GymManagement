using GymManagement.Domain.Admins.Events;
using GymManagment.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Events;

public class SubscriptionDeletedEventHandler(IGymRepository gymsRepository, IUnitOfWork unitOfWork) : INotificationHandler<SubscriptionDeletedEvent>
{
    private readonly IGymRepository _gymsRepository = gymsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var gyms = await _gymsRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);

        await _gymsRepository.RemoveRangeAsync(gyms);
        await _unitOfWork.CommitChangesAsync();
    }
}
