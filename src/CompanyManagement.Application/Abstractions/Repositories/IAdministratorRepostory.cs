using CompanyManagement.Domain.Administrators;

namespace CompanyManagement.Application.Abstractions.Repositories;

public interface IAdministratorRepostory
{
    Task AddAdministratorAsync(Administrator administrator, CancellationToken cancellationToken = default);
}
