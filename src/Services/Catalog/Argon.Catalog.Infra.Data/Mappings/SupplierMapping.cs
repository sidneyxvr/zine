using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable(nameof(Supplier));

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Ignore(s => s.DomainEvents);

            builder.Property(s => s.Name)
                .IsUnicode(false)
                .HasMaxLength(Supplier.NameMaxLength)
                .IsRequired(true);

            builder.Property(s => s.Address)
                .IsUnicode(false)
                .HasMaxLength(Supplier.AddressMaxLength)
                .IsRequired(true);

            builder.OwnsOne(a => a.Location, e =>
            {
                e.Property("_coordinate")
                    .HasColumnName("Location")
                    .HasColumnType("geography")
                    .IsRequired();
            });
        }
    }
}
