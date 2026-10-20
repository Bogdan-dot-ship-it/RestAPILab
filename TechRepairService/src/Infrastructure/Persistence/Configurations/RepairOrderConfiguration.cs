using Domain.Devices;
using Domain.RepairOrders;
using Domain.Technicians;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RepairOrderConfiguration : IEntityTypeConfiguration<RepairOrder>
{
    public void Configure(EntityTypeBuilder<RepairOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new RepairOrderId(x));

        builder.Property(x => x.OrderNumber)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasIndex(x => x.OrderNumber)
            .IsUnique();

        builder.Property(x => x.DeviceModel)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(x => x.EstimatedCost)
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(x => x.IsUrgent)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.EstimatedCompletionDate)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();

        builder.HasOne<Device>()
            .WithMany()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Technician>()
            .WithMany()
            .HasForeignKey(x => x.TechnicianId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}