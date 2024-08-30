using CompanyManagement.Application.Abstractions;
using CompanyManagement.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Application.Tests.Inegration.Infrastructure;

public class IntegrationTestBase : IClassFixture<IntegrationTestWebFactory>
{
    protected readonly ApplicationDbContext _dbContxet;
    protected readonly IComapnyRepository _comapnyRepository;
    private readonly IServiceScope _scope;


    public IntegrationTestBase(IntegrationTestWebFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _dbContxet = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _comapnyRepository = _scope.ServiceProvider.GetRequiredService<IComapnyRepository>();
    }
}
