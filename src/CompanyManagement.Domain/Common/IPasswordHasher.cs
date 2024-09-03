using ErrorOr;

namespace CompanyManagement.Domain.Common;

public interface IPasswordHasher
{
    ErrorOr<string> HashPassword(string password);

    bool IsCorrectPassword(string password, string hash);
}
