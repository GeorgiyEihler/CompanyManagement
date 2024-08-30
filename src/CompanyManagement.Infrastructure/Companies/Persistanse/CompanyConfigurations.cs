using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Shared.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Companies.Persistanse;

internal sealed class CompanyConfigurations : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .ToTable("companies");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(i => i.Id, v => CompanyId.Create(v));

        builder.Property(c => c.CreatedOn);
        builder.Property(c => c.ModifiedOn);

        builder
            .ComplexProperty(c => c.Number);

        builder
            .ComplexProperty(c => c.Name);

        builder.OwnsOne(c => c.Images, icb =>
        {
            icb.ToJson("image_collection");

            icb.OwnsMany(ic => ic.Images, ib =>
            {
                ib.Property(i => i.Alt);
                ib.Property(i => i.Url);
                ib.Property(i => i.Name);
            });
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder
            .HasIndex("_isDeleted")
            .HasFilter("is_deleted = false");

        builder.HasMany(c => c.Employees)
            .WithOne()
            .HasForeignKey(c => c.CompanyId);

        builder.Navigation(c => c.Employees)
            .AutoInclude();
    }
}
