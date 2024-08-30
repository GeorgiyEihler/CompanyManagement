using CompanyManagement.Api.AssemblyMarker;
using CompanyManagement.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace CompanyManagement.Application.Tests.Inegration.Infrastructure;

public class IntegrationTestWebFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("company_managment_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithName("company_managment_db")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.RemoveAll<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .LogTo(Console.WriteLine)
                    .UseSnakeCaseNamingConvention());
        });

        //builder.ConfigureAppConfiguration(configurationBuilder =>
        //{
        //    configurationBuilder.Sources.Clear();

        //    var jsonString = $$"""
        //    {
        //      "Logging": {
        //        "LogLevel": {
        //          "Default": "Information",
        //          "Microsoft.AspNetCore": "Warning"
        //        }
        //      },
        //      "ConnectionStrings": {
        //        "Database": "{{_dbContainer.GetConnectionString()}}"
        //      }
        //    }
        //    """;

        //    var stream = new MemoryStream();
        //    var writer = new StreamWriter(stream);
        //    writer.Write(jsonString);
        //    writer.Flush();
        //    stream.Position = 0;

        //    configurationBuilder.AddJsonStream(stream);
        //});

        //builder.ConfigureServices(services =>
        //{
        //    services.RemoveAll<ApplicationDbContext>();

        //    services.AddDbContext<ApplicationDbContext>(o =>
        //    {
        //        o.UseNpgsql(_dbContainer.GetConnectionString())
        //        .AddInterceptors(new SoftDeleteInterceptor());
        //    });
        //});
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
