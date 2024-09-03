using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CompanyManagement.Infrastructure.Authentication.TokenGenerators;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
        private readonly IConfiguration _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(AuthenticationConstants.JWT_SETTINGS_SECTION_NAME)
            .Bind(options);
    }
}
