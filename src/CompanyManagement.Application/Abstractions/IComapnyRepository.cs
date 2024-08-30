using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Abstractions;

public interface IComapnyRepository
{
    Task<bool> IsCompanyNumberExistsAsync(Number number, CancellationToken cancellationToken = default);
    Task AddCompanyAsync(Company company, CancellationToken cancellationToken = default);
    Task<ErrorOr<Company>> GetByIdAsync(CompanyId companyId, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Company comany, CancellationToken cancellationToken = default);
}
