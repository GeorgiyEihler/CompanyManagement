using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users.Events;
using CompanyManagement.Domain.Administrators;
using MediatR;

namespace CompanyManagement.Application.Administrators.CreateAdministrator;

internal sealed class AdministratorCreatedEventHandler : INotificationHandler<AdminCreatedEvent>
{
    private readonly IAdministratorRepostory _administratorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AdministratorCreatedEventHandler(
            IAdministratorRepostory administratorRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
    {
        _administratorRepository = administratorRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(AdminCreatedEvent notification, CancellationToken cancellationToken)
    {
        var administrator = new Administrator(AdministratorId.Create(notification.AdminId), _dateTimeProvider.UtcNow);

        await _administratorRepository.AddAdministratorAsync(administrator, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
