using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Owners;
using CompanyManagement.Infrastructure.Persistence;

namespace CompanyManagement.Infrastructure.Owners.Perisisntence;

internal sealed class OwnerRepositpry : IOwnerRepositpry
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OwnerRepositpry(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task AddOwnerAsync(Owner owner, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.Owners.AddAsync(owner, cancellationToken);
    }
}
