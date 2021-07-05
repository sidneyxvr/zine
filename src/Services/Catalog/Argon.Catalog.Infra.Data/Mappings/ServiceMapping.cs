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

            builder.OwnsMany(p => p.Images, i => 
            {
                i.ToTable(nameof(Image));

                i.Property<int>("Id")
                    .ValueGeneratedOnAdd();
                
                i.HasKey("Id");

                i.WithOwner()
                    .HasForeignKey("ServiceId");

                i.Property(f => f.Url)
                    .IsUnicode(false)
                    .HasMaxLength(Image.UrlMaxLength)
                    .IsRequired();
            });

            builder.OwnsMany(p => p.FeeHomeAssistances, i =>
            {
                i.ToTable(nameof(FeeHomeAssistance));

                i.WithOwner().HasForeignKey("ServiceId");

                i.Property<int>("Id");
                i.HasKey("Id");


                i.Property(f => f.Radius)
                    .IsRequired();

                i.Property(f => f.Price)
                    .HasPrecision(6, 2)
                    .IsRequired();
            });

            builder.HasMany(s => s.Tags)
                .WithMany(t => t.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceTag",
                    s => s.HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict),
                    s => s.HasOne<Service>()
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                );
        }
    }
}
