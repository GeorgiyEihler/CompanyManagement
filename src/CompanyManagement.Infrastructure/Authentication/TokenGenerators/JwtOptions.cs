namespace CompanyManagement.Infrastructure.Authentication.TokenGenerators;

public class JwtOptions
{
    public string Audience { get; set; } = default!;

    public string Issuer { get; set; } = default!;

    public string Secrect { get; set; } = default!;

    public int TokenExpirationInMinites { get; set; } 
}
