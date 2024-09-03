using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Owners;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users.Events;
using MediatR;

namespace CompanyManagement.Application.Owners.CreateOwner;

internal sealed class OwnerCreatedEventHandler : INotificationHandler<OwnerCreatedEvent>
{
    private readonly IOwnerRepositpry _ownerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OwnerCreatedEventHandler(
        IOwnerRepositpry ownerRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _ownerRepository = ownerRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(OwnerCreatedEvent notification, CancellationToken cancellationToken)
    {
        var owner = new Owner(OwnerId.Create(notification.OwnerId), _dateTimeProvider.UtcNow);

        await _ownerRepository.AddOwnerAsync(owner, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
