using Bogus;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Companies.Employees;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users;
using CompanyManagement.Infrastructure.Persistence;

namespace CompanyManagement.Api.Extensions;

public static class SeedDataExtension
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        var faker = new Faker();

        using var scope = app.ApplicationServices.CreateScope();

        var dateTimeProvider = scope.ServiceProvider.GetRequiredService<IDateTimeProvider>();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var companyList = new List<Company>();  

        for (var i = 0; i < 10; i++)
        {
            var companyId = CompanyId.NewId;

            var companyNumber = new Number(faker.Random.Number(100000));

            var companyName = Name.Create(faker.Company.CompanyName()).Value;

            var company = new Company(
                companyId,
                dateTimeProvider.UtcNow,
                companyName,
                companyNumber,
                ImagesCollection.Create([Image.Create(faker.Name.FirstName(), faker.Internet.Url(), faker.Name.FirstName()).Value]));

            for (var j = 0; j < 1_000 ; j++)
            {
                var employeeId = EmployeeId.NewId;

                var email = Email.Create(
                    faker.Person.Email).Value;

                var fullName = Domain.Companies.Employees.FullName.Create(
                    faker.Person.FirstName, 
                    faker.Person.LastName, 
                    faker.Person.LastName);

                var employee = new Employee(
                    employeeId,
                    dateTimeProvider.UtcNow,
                    email,
                    fullName,
                    companyId,
                    EmployeeStatus.FromValue(1),
                    ProjectCollection.Create([new(faker.Name.FirstName(), faker.Person.Company.Name, DateOnly.FromDateTime(DateTime.Now.AddDays(-100)), DateOnly.FromDateTime(DateTime.Now))]));

                company.SetEmployee([employee]);
            }

            companyList.Add(company);
        }

        await dbContext.Companys.AddRangeAsync(companyList);

        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        var userList = new List<User>();

        for (var i = 0; i < 10; i++)
        {
            var password = "qewQWE123!@#";

            var user = new User(
                UserId.NewId,
                DateTime.UtcNow,
                Domain.Users.FullName.Create(faker.Name.FirstName(), faker.Name.LastName(), faker.Name.LastName()).Value,
                Login.Create(faker.Internet.UserName()).Value,
                Email.Create(faker.Person.Email).Value,
                hasher.HashPassword(password).Value);

            userList.Add(user);
        }

        await dbContext.Users.AddRangeAsync(userList);

        await dbContext.SaveChangesAsync();
    }
}
