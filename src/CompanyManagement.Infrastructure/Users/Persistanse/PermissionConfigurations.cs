using CompanyManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Users.Persistanse;

internal sealed class PermissionConfigurations : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code);

        builder.HasData(
            Permission.GetCompany,
            Permission.UpdateCompany,
            Permission.DeleteCompany,
            Permission.GetUser,
            Permission.CreateAdministator,
            Permission.UpdateUser);

        builder.HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permission");

                joinBuilder.HasData(
                    CreateRolePermission(Role.Owner,Permission.GetCompany),
                    CreateRolePermission(Role.Owner,Permission.UpdateCompany),
                    CreateRolePermission(Role.Owner,Permission.DeleteCompany),
                    CreateRolePermission(Role.Administrator, Permission.GetCompany),
                    CreateRolePermission(Role.Administrator, Permission.UpdateCompany),
                    CreateRolePermission(Role.Administrator, Permission.DeleteCompany),
                    CreateRolePermission(Role.Administrator, Permission.GetUser),
                    CreateRolePermission(Role.Administrator, Permission.CreateAdministator),
                    CreateRolePermission(Role.Administrator, Permission.UpdateUser),
                    CreateRolePermission(Role.Participatn,  Permission.GetCompany));
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
