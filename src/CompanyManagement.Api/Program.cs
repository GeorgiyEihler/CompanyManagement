using CompanyManagement.Api.Extensions;
using CompanyManagement.Application;
using CompanyManagement.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddJwtSwaggerGen();

builder.Services.AddGlobalErrorHandling();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;

        o.TokenValidationParameters = new()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secrect"]!)),
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"]!,
            ValidAudience = builder.Configuration["JwtSettings:Audience"]!,
            ClockSkew = TimeSpan.Zero,
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigratinosAsync();
    await app.SeedDataAsync();
}

app.UseGlobalErrorHandling();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();