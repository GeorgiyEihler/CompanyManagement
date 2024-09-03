using CompanyManagement.Domain.Users;
using ErrorOr;

namespace CompanyManagement.Application.Abstractions;

public interface IJwtGenerator
{
    Task<ErrorOr<string>> GenerateToken(User user);
}
