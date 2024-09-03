using CompanyManagement.Domain.Common;

namespace CompanyManagement.Infrastructure.Clock;
internal sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}