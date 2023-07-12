using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.ValueObjects;
using TeamManager.Core.Users.Models;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Infrastructure.DAL.Configurations;

public class TimerConfiguration : IEntityTypeConfiguration<Timer>
{
    public void Configure(EntityTypeBuilder<Timer> builder)
    {
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        builder.HasOne<Project>().WithMany().HasForeignKey(x => x.Project);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new Description(x))
            .IsRequired();
        builder.Property(x => x.Date)
            .IsRequired();
    }
}