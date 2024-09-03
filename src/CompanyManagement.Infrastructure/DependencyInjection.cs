using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Infrastructure.Authentication;
using CompanyManagement.Infrastructure.Authentication.TokenGenerators;
using CompanyManagement.Infrastructure.Clock;
using CompanyManagement.Infrastructure.Companies.Persistanse;
using CompanyManagement.Infrastructure.Persistence;
using CompanyManagement.Infrastructure.Users.Persistanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompanyManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ??
            throw new ApplicationException(nameof(configuration));

        services.AddTransient<IDateTimeProvider, SystemDateTimeProvider>();

        services.ConfigureOptions<JwtOptionsSetup>();

        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHanser>();

        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); })));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IComapnyRepository, ComapnyRepositories>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}