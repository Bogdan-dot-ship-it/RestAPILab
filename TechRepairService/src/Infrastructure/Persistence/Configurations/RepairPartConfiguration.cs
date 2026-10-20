using Domain.RepairOrders;
using Domain.RepairParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RepairPartConfiguration : IEntityTypeConfiguration<RepairPart>
{
    public void Configure(EntityTypeBuilder<RepairPart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RepairOrderId)
    .HasConversion(x => x.Value, x => new RepairOrderId(x))
    .IsRequired();

        builder.HasOne<RepairOrder>()
            .WithMany()
            .HasForeignKey(x => x.RepairOrderId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.PartName)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnType("integer")
            .HasDefaultValue(1)
            .IsRequired();
    }
}