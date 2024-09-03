using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Administrators;
using CompanyManagement.Infrastructure.Persistence;

namespace CompanyManagement.Infrastructure.Administrators.Persistence;

internal sealed class AdministartorRepository : IAdministratorRepostory
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AdministartorRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task AddAdministratorAsync(Administrator administrator, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.Administrators.AddAsync(administrator, cancellationToken);
    }
}
