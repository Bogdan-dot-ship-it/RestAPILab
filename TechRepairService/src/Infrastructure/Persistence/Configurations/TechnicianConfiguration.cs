using Domain.Technicians;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
    public void Configure(EntityTypeBuilder<Technician> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(x => x.Specialization)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(x => x.Phone)
            .IsUnique();

        builder.Property(x => x.IsActive)
            .HasColumnType("boolean")
            .HasDefaultValue(true)
            .IsRequired();
    }
}