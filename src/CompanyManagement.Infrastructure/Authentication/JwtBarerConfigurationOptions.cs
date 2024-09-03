using CompanyManagement.Infrastructure.Authentication.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CompanyManagement.Infrastructure.Authentication;

internal sealed class JwtBarerConfigurationOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public JwtBarerConfigurationOptions(
        IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secrect)),
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            ClockSkew = TimeSpan.Zero,
        };
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(options);
    }
}