﻿using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Shared.Ids;
using ErrorOr;

namespace CompanyManagement.Application.Comanies.CreateCompany;

public sealed class CreateCompanyHandler
{
    public CreateCompanyHandler(
        IComapnyRepository comapnyRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _companyRepository = comapnyRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }


    private readonly IComapnyRepository _companyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<ErrorOr<Guid>> HandleAsync(CreateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var image = command.Images.Select(i => Image.Create(i.Name, i.Alt, i.Url).Value);

        var name = Name.Create(command.Name).Value;

        var number = new Number(command.Number);

        if (await _companyRepository.IsCompanyNumberExistsAsync(number, cancellationToken))
        {
            return Error.Conflict(code: "CreateCompany.Number.AlreadyExists", description: "A company with the same number already exists");
        }

        var company = new Company(
            CompanyId.NewId,
            _dateTimeProvider.UtcNow,
            name,
            number,
            ImagesCollection.Create(image));

        await _companyRepository.AddCompanyAsync(company, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return company.Id.Id;
    }
}
