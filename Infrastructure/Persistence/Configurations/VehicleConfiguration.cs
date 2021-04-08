using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(t => t.Engine)
                .HasMaxLength(200);

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(450);

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(450);
        }
    }
}
