using CompanyManagement.Application.Comanies.GetCompany;
using CompanyManagement.Application.Tests.Inegration.Infrastructure;
using ErrorOr;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Application.Tests.Inegration.Companies;

public class GetCompanyByIdTests : IntegrationTestBase
{
    private readonly GetCompanyHandler _sut;

    public GetCompanyByIdTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _sut = new GetCompanyHandler(_comapnyRepository);
    }

    [Fact]
    public async Task HandleAsync_WhenGetExistingCompany_ShouldReturnValidCompany()
    {
        var id = _dbContxet.Companys.AsNoTracking().First().Id.Id;

        var query = new GetCompanyQuery(id);

        var company = await _sut.HandleAsync(query);

        company.Value.Id.Should().Be(id);
    }

    [Fact]
    public async Task HandleAsync_WhenGetNotExistingCompany_ShouldReturnNotFoundError()
    {
        var id = Guid.NewGuid();

        var query = new GetCompanyQuery(id);

        var company = await _sut.HandleAsync(query);

        company.IsError.Should().BeTrue();

        company.FirstError.Should().Be(Error.NotFound());
    }
}
