using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Comanies.RemoveCompany;

public class RemoveCompanyHandler
{
    private readonly IComapnyRepository _comapnyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCompanyHandler(
        IComapnyRepository comapnyRepository, 
        IUnitOfWork unitOfWork)
    {
        _comapnyRepository = comapnyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RemoveCompanyResponse>> HandleAsync(RemoveCompayCommand request, CancellationToken cancellationToken = default)
    {
        var companyId = CompanyId.Create(request.CompanyId);

        var companyResult = await _comapnyRepository.GetByIdAsync(companyId, cancellationToken);

        if (companyResult.IsError)
        {
            return Error.NotFound();
        }

        companyResult.Value.Delete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RemoveCompanyResponse(request.CompanyId);
    }
}
