using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.ValueObjects;
using TeamManager.Core.Users.Models;

namespace TeamManager.Infrastructure.DAL.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.Property(x => x.Label)
            .HasConversion(x => x.Value, x => new Label(x))
            .IsRequired();
        builder.Property(x => x.Color)
            .HasConversion(x => x.Value, x => new Color(x))
            .IsRequired();
    }
}