using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class ServiceMapping : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable(nameof(Service));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.Name)
                .IsUnicode(false)
                .HasMaxLength(Service.NameMaxLength)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsUnicode(false)
                .HasMaxLength(Service.DescriptionMaxLength);

            builder.Property(p => p.Price)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Services)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.SubCategory)
                .WithMany(c => c.Services)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata
                .FindNavigation(nameof(Service.FeeHomeAssistances))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(Service.Images))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.Images)
                .WithOne(f => f.Service)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.FeeHomeAssistances)
                .WithOne(f => f.Service)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
