using CompanyManagement.Application.Abstractions;
using CompanyManagement.Domain.Administrators;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Companies;
using CompanyManagement.Domain.Owners;
using CompanyManagement.Domain.Participants;
using CompanyManagement.Domain.Users;
using CompanyManagement.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Infrastructure.Persistence;

public sealed class ApplicationDbContext: DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Company> Companys => Set<Company>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Participant> Participants => Set<Participant>();

    public DbSet<Administrator> Administrators => Set<Administrator>();

    public DbSet<Owner> Owners => Set<Owner>();

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

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        await ProcessDomainEvents();

        return result;
    }

    private async Task ProcessDomainEvents()
    {
        var domainEvents = ChangeTracker.Entries<IAggregateRoot>()
            .SelectMany(a => a.Entity.PopDomainEvents())
            .ToList();

        if (domainEvents.Count == 0)
        {
            return;
        }

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}