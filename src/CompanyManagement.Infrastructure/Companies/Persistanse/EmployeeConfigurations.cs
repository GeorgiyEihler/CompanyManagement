using CompanyManagement.Domain.Companies.Employees;
using CompanyManagement.Domain.Shared.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Companies.Persistanse;

internal sealed class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(e => e.Id);

        builder
           .Property(c => c.Id)
           .HasConversion(i => i.Id, v => EmployeeId.Create(v));

        builder.Property(c => c.CreatedOn);
        builder.Property(c => c.ModifiedOn);

        builder.Property(e => e.CompanyId)
            .HasConversion(i => i.Id, v => CompanyId.Create(v));

        builder.Property(s => s.EmployeeStatus)
           .HasConversion(
               employeeStatus => employeeStatus.Value,
               value => EmployeeStatus.FromValue(value));


        builder.ComplexProperty(e => e.Email);

        builder.ComplexProperty(e => e.FullName);

        builder.OwnsOne(e => e.Projects, pcb =>
        {
            pcb.ToJson("project_collection");

            pcb.OwnsMany(pc => pc.Projects, pb =>
            {
                pb.Property(i => i.StartDate);
                pb.Property(i => i.Name);
                pb.Property(i => i.EndDate);
                pb.Property(i => i.Position);
            });
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
