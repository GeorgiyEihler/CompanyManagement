using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Application.Tests.Inegration.Infrastructure;

public class IntegrationTestBase : IClassFixture<IntegrationTestWebFactory>
{
    protected readonly ApplicationDbContext _dbContxet;

    protected readonly IComapnyRepository _comapnyRepository;
    protected readonly IUserRepository _userRepository;
    protected readonly IUnitOfWork _unitOfWork;

    protected readonly IPasswordHasher _passwordHasher;
    protected readonly IJwtGenerator _jwtGenerator;

    private readonly IServiceScope _scope;

    public IntegrationTestBase(IntegrationTestWebFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _dbContxet = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        _comapnyRepository = _scope.ServiceProvider.GetRequiredService<IComapnyRepository>();
        _userRepository = _scope.ServiceProvider.GetRequiredService<IUserRepository>();
        _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        _jwtGenerator = _scope.ServiceProvider.GetRequiredService<IJwtGenerator>();
        _passwordHasher = _scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    }
}
