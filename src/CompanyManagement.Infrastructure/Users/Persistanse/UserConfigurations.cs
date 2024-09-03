using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Users.Persistanse;

internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users");

        builder
            .HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(i => i.Id, v => UserId.Create(v));

        builder.Property(u => u.OwnerId)
            .HasConversion(i => i.Id, v => OwnerId.Create(v));

        builder.Property(u => u.ParticipantId)
            .HasConversion(i => i.Id, v => ParticipantId.Create(v));

        builder.Property(u => u.AdministratorId)
            .HasConversion(i => i.Id, v => AdministratorId.Create(v));

        builder.ComplexProperty(u => u.FullName);

        builder.ComplexProperty(u => u.Email);

        builder.ComplexProperty(u => u.Login);

        builder.Property<string>("_passwordHash")
            .HasColumnName("password_hash");
    }
}
