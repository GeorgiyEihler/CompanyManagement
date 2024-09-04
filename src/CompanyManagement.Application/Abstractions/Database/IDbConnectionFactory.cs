using System.Data.Common;

namespace CompanyManagement.Application.Abstractions.Database;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}