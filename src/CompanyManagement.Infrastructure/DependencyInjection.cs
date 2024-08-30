using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Common;
using CompanyManagement.Infrastructure.Cloack;
using CompanyManagement.Infrastructure.Companies.Persistanse;
using CompanyManagement.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); })));

        services.AddScoped<IComapnyRepository, ComapnyRepositories>();

        return services;
    }
}