﻿using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Common;
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

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new(JwtRegisteredClaimNames.Name, user.FullName.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.FullName.LastName),
                new(JwtRegisteredClaimNames.Email, user.Email.Value),
                new(SpecialClaims.UserId, user.Id.Id.ToString()!),
            ]),
            Expires = _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinites),
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
            SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(tokenDescription);
    }
}