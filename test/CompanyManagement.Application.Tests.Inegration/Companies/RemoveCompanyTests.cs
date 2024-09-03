using CompanyManagement.Application.Comanies.RemoveCompany;
using CompanyManagement.Application.Tests.Inegration.Infrastructure;
using ErrorOr;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Application.Tests.Inegration.Companies;

public class RemoveCompanyTests : IntegrationTestBase
{
    private readonly RemoveCompanyHandler _sut;
    public RemoveCompanyTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _sut = new RemoveCompanyHandler(_comapnyRepository, _unitOfWork);
    }

    [Fact]
    public async Task HandleAsync_WhenRemoveNotExistiongCompany_ShouldReturnNotFoundError()
    {
        var id = Guid.NewGuid();

        var removeCompanyRequest = new RemoveCompayCommand(id);

        var result = await _sut.HandleAsync(removeCompanyRequest);

        result.IsError.Should().BeTrue();

        result.FirstError.Should().Be(Error.NotFound());
    }

    [Fact]
    public async Task HandleAsync_WhenRemoveExistiongCompanyWithToJsonConfiguration_ShouldNotThrowInvalidOperationException()
    {
        var id = (await _dbContxet.Companys.AsNoTracking().FirstAsync()).Id.Id;

        var removeCompanyRequest = new RemoveCompayCommand(id);

        var action = async () => await _sut.HandleAsync(removeCompanyRequest);

        await action.Should().NotThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task HandleAsync_WhenRemoveExistiongCompanyWithToJsonConfiguration_ShouldRemoveSoftDeleteAggregate()
    {
        var id = (await _dbContxet.Companys.AsNoTracking().FirstAsync()).Id.Id;

        var removeCompanyRequest = new RemoveCompayCommand(id);

        var removeResult = await _sut.HandleAsync(removeCompanyRequest);

        var company = await _dbContxet.Companys.FirstOrDefaultAsync(c => c.Id == id);

        removeResult.IsError.Should().BeFalse();
        removeResult.Value.CompanyId.Should().Be(id);
        company.Should().BeNull();
    }
}