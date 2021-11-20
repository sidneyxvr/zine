using Argon.Zine.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Zine.Catalog.Infra.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Ignore(p => p.DomainEvents);

        builder.Property(p => p.Name)
            .IsUnicode(false)
            .HasMaxLength(Product.NameMaxLength)
            .IsRequired();

        builder.Property(p => p.Description)
            .IsUnicode(false)
            .HasMaxLength(Product.DescriptionMaxLength);

        builder.Property(p => p.Price)
            .HasPrecision(6, 2)
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .IsUnicode(false)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasOne(p => p.Restaurant)
            .WithMany(s => s.Services)
            .HasForeignKey(p => p.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}