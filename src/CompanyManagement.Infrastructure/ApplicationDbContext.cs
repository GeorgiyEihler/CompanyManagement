using CompanyManagement.Domain.Companies;
using CompanyManagement.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Company> Companys => Set<Company>();

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}