using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Core.Assignments.Models;
using TeamManager.Core.Assignments.ValueObjects;
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Infrastructure.DAL.Configurations;

internal sealed class AssignmentsConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new AssignmentName(x));
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new AssignmentDescription(x));
        builder.Property(x => x.Priority)
            .HasConversion(x => x.Value, x => new AssignmentPriority(x));
    }
}