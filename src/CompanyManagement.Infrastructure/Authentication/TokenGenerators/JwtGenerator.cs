using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Administrators;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Owners;
using CompanyManagement.Domain.Participants;
using CompanyManagement.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CompanyManagement.Infrastructure.Authentication.TokenGenerators;

internal sealed class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _jwtOptions;

    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtGenerator(
        IOptions<JwtOptions> jwtSettings,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtOptions = jwtSettings.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secrect));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = GenerateUserClaims(user);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinites),
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
            SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(tokenDescription);
    }

    private List<Claim> GenerateUserClaims(User user)
    { 
        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Name, user.FullName.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.FullName.LastName),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new("id", user.Id.Id.ToString()!),
        ];

        claims
            .AddRoleClaims("adminId", user.AdministratorId?.Id.ToString(), nameof(Administrator))
            .AddRoleClaims("ownerId", user.OwnerId?.Id.ToString(), nameof(Owner))
            .AddRoleClaims("participantId", user.ParticipantId?.Id.ToString(), nameof(Participant));
        
        return claims;
    }
}

public static class ClaimExtension
{
    public static List<Claim> AddRoleClaims(this List<Claim> claims, string claimName, string? value, string roleName)
    {
        if (value is null)
        {
            return claims;
        }

        claims.Add(new Claim(claimName, value));

        if (roleName == nameof(Administrator))
        {
            claims.Add(new Claim(SpecialClaims.Permission, "companies:create"));
            claims.Add(new Claim(SpecialClaims.Permission, "companies:remove"));
            claims.Add(new Claim(SpecialClaims.Permission, "companies:read"));
            claims.Add(new Claim(SpecialClaims.Permission, "companies:read"));
            claims.Add(new Claim(SpecialClaims.Permission, "administrators:create"));
            claims.Add(new Claim(SpecialClaims.Permission, "owners:create"));
        }

        if (roleName == nameof(Owner))
        {
            claims.Add(new Claim(SpecialClaims.Permission, "companies:create"));
            claims.Add(new Claim(SpecialClaims.Permission, "companies:remove"));
            claims.Add(new Claim(SpecialClaims.Permission, "companies:read"));
        }

        if (roleName == nameof(Participant))
        {
            claims.Add(new Claim(SpecialClaims.Permission, "companies:read"));
        }

        return claims;
    }
}