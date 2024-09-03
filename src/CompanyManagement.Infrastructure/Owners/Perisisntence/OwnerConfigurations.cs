using CompanyManagement.Domain.Owners;
using CompanyManagement.Domain.Shared.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Owners.Perisisntence;

internal sealed class OwnerConfigurations : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("owners");

        builder.HasKey(e => e.Id);

        builder
           .Property(c => c.Id)
           .HasConversion(i => i.Id, v => OwnerId.Create(v));

        builder.Property(c => c.CreatedOn);
        builder.Property(c => c.ModifiedOn);
    }
}
