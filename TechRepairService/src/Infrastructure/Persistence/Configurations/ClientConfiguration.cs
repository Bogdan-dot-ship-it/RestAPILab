using Domain.Clients;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(x => x.Phone)
            .IsUnique();

        builder.Property(x => x.Email)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();
    }
}