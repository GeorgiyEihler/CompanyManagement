using CompanyManagement.Application.AssemblyMarker;
using CompanyManagement.Application.Comanies.CreateCompany;
using CompanyManagement.Application.Comanies.GetCompany;
using CompanyManagement.Application.Comanies.RemoveCompany;
using CompanyManagement.Application.Users.ChangePassword;
using CompanyManagement.Application.Users.Login;
using CompanyManagement.Application.Users.Register;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(IApplicationAssamblyMarker).Assembly);
        });

        services.AddScoped<GetCompanyHandler>();
        services.AddScoped<RemoveCompanyHandler>();
        services.AddScoped<CreateCompanyHandler>();
        services.AddScoped<LoginHandler>();
        services.AddScoped<ChangePasswordHandler>();
        services.AddScoped<RegisterHander>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
