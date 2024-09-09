using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Infrastructure.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Infrastructure.Companies.Persistanse;

internal sealed class ComapnyRepositories(ApplicationDbContext context) : IComapnyRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> IsCompanyNumberExistsAsync(Number number, CancellationToken cancellationToken = default)
    {
        var isExists = await _context.Companys
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Number == number, cancellationToken);

        return isExists is not null;
    }

    public async Task AddCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        await _context.Companys.AddAsync(company);
    }

    public async Task<ErrorOr<Company>> GetByIdAsync(CompanyId companyId, CancellationToken cancellationToken = default)
    {
        var company = await _context.Companys.FirstOrDefaultAsync(c => c.Id == companyId, cancellationToken);

        if (company is null)
        {
            return Error.NotFound();
        }

        return company;
    }

    public bool Remove(Company comany)
    {
        _context.Companys.Remove(comany);

        return true;
    }
}