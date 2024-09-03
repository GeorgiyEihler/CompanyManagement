using CompanyManagement.Application.Abstractions.Database;
using Npgsql;
using System.Data.Common;

namespace CompanyManagement.Infrastructure.Persistence;

internal sealed class DbConnectinoFatroy : IDbConnectionFactory
{
    private readonly NpgsqlDataSource _dataSource;

    public DbConnectinoFatroy(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }
}
