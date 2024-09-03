using CompanyManagement.Domain.Users;

namespace CompanyManagement.Application.Abstractions;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}
