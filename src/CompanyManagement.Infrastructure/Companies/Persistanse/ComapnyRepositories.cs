using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Shared.Ids;
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

        await _context.SaveChangesAsync();
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

    public async Task<bool> RemoveAsync(Company comany, CancellationToken cancellationToken = default)
    {
        _context.Companys.Remove(comany);

        await _context.SaveChangesAsync(cancellationToken);

        return true;    
    }
}