using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Comanies.GetCompany;

public sealed class GetCompanyHandler
{
    private readonly IComapnyRepository _comapnyRepository;

    public GetCompanyHandler(IComapnyRepository comapnyRepository)
    {
        _comapnyRepository = comapnyRepository;
    }

    public async Task<ErrorOr<GetCompanyResponse>> HandleAsync(GetCompanyQuery request, CancellationToken cancellationToken = default)
    {
        var companyId = CompanyId.Create(request.CompanyId);

        var companyResult = await _comapnyRepository.GetByIdAsync(companyId);

        if (companyResult.IsError)
        {
            return Error.NotFound();
        }

        var employeeResponse = companyResult.Value.Employees
            ?.Select(e => new EmployeeResponse(e.Id.Id, e.FullName.FirstName, e.FullName.LastName, e.Email.Value, e.EmployeeStatus.Name)) ?? [];

        var imageCollectionResponse = companyResult.Value.Images?.Images?.Select(i => new ImageResponse(i.Url, i.Name)) ?? [];
            

        var response = new GetCompanyResponse(
            companyResult.Value.Id.Id,
            companyResult.Value.Name.Value,
            companyResult.Value.Number.Value,
            [..employeeResponse],
            new(imageCollectionResponse.ToList()));

        return response;
    }

}
