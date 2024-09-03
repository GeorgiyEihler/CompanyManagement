using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CompanyManagement.Infrastructure.Authentication.TokenGenerators;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    public const string SECTION = "JwtSettings";

    private readonly IConfiguration _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SECTION)
            .Bind(options);
    }
}
