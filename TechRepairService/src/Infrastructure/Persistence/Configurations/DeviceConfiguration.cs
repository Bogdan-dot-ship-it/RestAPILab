using Domain.Clients;
using Domain.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClientId)
            .IsRequired();

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Brand)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.Model)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(x => x.SerialNumber)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasIndex(x => x.SerialNumber)
            .IsUnique();

        builder.Property(x => x.DeviceType)
            .HasColumnType("varchar(50)")
            .IsRequired();
    }
}