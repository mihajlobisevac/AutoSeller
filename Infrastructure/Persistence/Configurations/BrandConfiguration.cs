using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Country)
                .HasMaxLength(200);

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(450);

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(450);
        }
    }
}
