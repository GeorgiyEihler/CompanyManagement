using CompanyManagement.Domain.Administrators;
using CompanyManagement.Domain.Shared.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Administrators.Persistence;

internal sealed class AdministratorConfigurations : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("administrators");

        builder.HasKey(e => e.Id);

        builder
           .Property(c => c.Id)
           .HasConversion(i => i.Id, v => AdministratorId.Create(v));

        builder.Property(c => c.CreatedOn);
        builder.Property(c => c.ModifiedOn);
    }
}
