using Argon.Zine.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Zine.Customers.Infra.Data.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(nameof(Address));

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Ignore(c => c.DomainEvents);

        builder.Property(a => a.City)
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder.Property(a => a.Complement)
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        builder.Property(a => a.Country)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(a => a.District)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(a => a.Number)
            .HasColumnType("varchar(10)");

        builder.Property(a => a.PostalCode)
            .HasColumnType("char(8)")
            .IsRequired();

        builder.Property(a => a.State)
            .HasColumnType("char(2)")
            .IsRequired();

        builder.Property(a => a.Street)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.OwnsOne(a => a.Location, e =>
        {
            e.Property("_coordinate")
                .IsRequired()
                .HasColumnName("Location")
                .HasColumnType("geography");
        });
    }
}