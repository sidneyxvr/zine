using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Argon.Catalog.Infra.Data.Mappings
{
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

            builder.Property(p => p.Price)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsMany(p => p.Images, i => 
            {
                i.ToTable(nameof(Image));

                i.Property<Guid>("Id");
                i.HasKey("Id");

                i.WithOwner()
                    .HasForeignKey("ProductId");

                i.Property(f => f.Url)
                    .IsUnicode(false)
                    .HasMaxLength(Image.UrlMaxLength)
                    .IsRequired();
            });
        }
    }
}
