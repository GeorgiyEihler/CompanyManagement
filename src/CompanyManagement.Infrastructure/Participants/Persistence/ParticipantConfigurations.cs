using CompanyManagement.Domain.Participants;
using CompanyManagement.Domain.Shared.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Infrastructure.Participants.Persistence;

internal sealed class ParticipantConfigurations : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("participants");

        builder.HasKey(e => e.Id);

        builder
           .Property(c => c.Id)
           .HasConversion(i => i.Id, v => ParticipantId.Create(v));

        builder.Property(c => c.CreatedOn);
        builder.Property(c => c.ModifiedOn);
    }
}
