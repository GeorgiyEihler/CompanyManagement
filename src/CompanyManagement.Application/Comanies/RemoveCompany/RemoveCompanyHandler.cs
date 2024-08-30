using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Comanies.RemoveCompany;

public class RemoveCompanyHandler
{
    private readonly IComapnyRepository _comapnyRepository;

    public RemoveCompanyHandler(IComapnyRepository comapnyRepository)
    {
        _comapnyRepository = comapnyRepository;
    }

    public async Task<ErrorOr<RemoveCompanyResponse>> HandleAsync(RemoveCompayCommand request, CancellationToken cancellationToken = default)
    {
        var companyId = CompanyId.Create(request.CompanyId);

        var companyResult = await _comapnyRepository.GetByIdAsync(companyId, cancellationToken);

        if (companyResult.IsError)
        {
            return Error.NotFound();
        }

        await _comapnyRepository.RemoveAsync(companyResult.Value, cancellationToken);

        return new RemoveCompanyResponse(request.CompanyId);
    }
}
