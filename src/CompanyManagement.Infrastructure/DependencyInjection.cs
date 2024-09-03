using CompanyManagement.Application.Abstractions;
using CompanyManagement.Application.Abstractions.Authentication;
using CompanyManagement.Application.Abstractions.Database;
using CompanyManagement.Application.Abstractions.Repositories;
using CompanyManagement.Domain.Common;
using CompanyManagement.Infrastructure.Administrators.Persistence;
using CompanyManagement.Infrastructure.Authentication;
using CompanyManagement.Infrastructure.Authentication.TokenGenerators;
using CompanyManagement.Infrastructure.Authorization;
using CompanyManagement.Infrastructure.Clock;
using CompanyManagement.Infrastructure.Companies.Persistanse;
using CompanyManagement.Infrastructure.Owners.Perisisntence;
using CompanyManagement.Infrastructure.Participants.Persistence;
using CompanyManagement.Infrastructure.Persistence;
using CompanyManagement.Infrastructure.Users.Persistanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CompanyManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ??
            throw new ApplicationException(nameof(configuration));

        var npgsqlDataSource = new NpgsqlDataSourceBuilder(connectionString).Build();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectinoFatroy>();

        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();

        services.AddTransient<IDateTimeProvider, SystemDateTimeProvider>();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBarerConfigurationOptions>();

        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHanser>();

        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); })));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IComapnyRepository, ComapnyRepositories>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOwnerRepositpry, OwnerRepositpry>();
        services.AddScoped<IAdministratorRepostory, AdministartorRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();

        return services;
    }
}