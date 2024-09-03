using CompanyManagement.Domain.Owners;

namespace CompanyManagement.Application.Abstractions.Repositories;

public interface IOwnerRepositpry
{
    Task AddOwnerAsync(Owner owner, CancellationToken cancellationToken = default);
}
