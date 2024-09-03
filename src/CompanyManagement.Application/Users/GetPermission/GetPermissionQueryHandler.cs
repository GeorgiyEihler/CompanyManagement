using CompanyManagement.Application.Abstractions.Authentication;
using CompanyManagement.Application.Abstractions.Database;
using Dapper;
using ErrorOr;
using MediatR;
using System.Data.Common;

namespace CompanyManagement.Application.Users.GetPermission;

public sealed class GetPermissionQueryHandler
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetPermissionQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<ErrorOr<PermissionsResponse>> HandleAsync(GetPermissionQuery query, CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await _connectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT DISTINCT
                 u.id AS {nameof(UserQueryClass.UserId)},
                 rp.permission_code AS {nameof(UserQueryClass.Permission)}
             FROM public.users u
             JOIN public.user_roles ur ON ur.user_id = u.id
             JOIN public.role_permission rp ON rp.role_name = ur.role_name
             WHERE u.id = @UserId
             """
        ;

        List<UserQueryClass> permissions = (await connection.QueryAsync<UserQueryClass>(sql, query)).AsList();

        return new PermissionsResponse(permissions.Select(p => p.Permission).ToHashSet());
    }

    internal sealed class UserQueryClass
    {
        internal Guid UserId { get; init; }

        internal string Permission { get; init; }
    }
}
