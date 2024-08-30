using CompanyManagement.Domain.Common;

namespace CompanyManagement.Infrastructure.Cloack;
internal sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
