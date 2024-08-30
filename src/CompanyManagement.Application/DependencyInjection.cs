using CompanyManagement.Application.Comanies.CreateCompany;
using CompanyManagement.Application.Comanies.GetCompany;
using CompanyManagement.Application.Comanies.RemoveCompany;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetCompanyHandler>();
        services.AddScoped<RemoveCompanyHandler>();
        services.AddScoped<CreateCompanyHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
