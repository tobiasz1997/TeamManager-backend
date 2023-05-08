using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Models;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Infrastructure.DAL.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.HasIndex(x => x.Token).IsUnique();
        builder.Property(x => x.Token)
            .HasConversion(x => x.Value, x => new Token(x))
            .IsRequired();
        builder.HasIndex(x => x.Token).IsUnique();
        builder.Property(x => x.ExpiresAt).IsRequired();
    }
}