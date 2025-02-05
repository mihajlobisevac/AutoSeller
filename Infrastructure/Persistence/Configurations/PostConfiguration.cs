﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(2000)
                .IsUnicode();

            builder.Property(t => t.Location)
                .HasMaxLength(200);

            builder.Property(t => t.Engine)
                .HasMaxLength(200);

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(450);

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(450);
        }
    }
}
