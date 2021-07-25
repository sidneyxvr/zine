using Argon.Restaurants.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Restaurants.Infra.Data.Mappings
{
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
                .IsUnicode(false)
                .HasMaxLength(Address.CityMaxLength)
                .IsRequired();

            builder.Property(a => a.Complement)
                .IsUnicode(false)
                .HasMaxLength(Address.ComplementMaxLength);

            builder.Property(a => a.Country)
                .IsUnicode(false)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.District)
                .IsUnicode(false)
                .HasMaxLength(Address.DistrictMaxLength)
                .IsRequired();

            builder.Property(a => a.Number)
                .IsUnicode(false)
                .HasMaxLength(Address.NumberMaxLength);

            builder.Property(a => a.PostalCode)
                .HasColumnType($"char({Address.PostalCodeLength})")
                .IsRequired();

            builder.Property(a => a.State)
                .HasColumnType($"char({Address.StateLength})")
                .IsRequired();

            builder.Property(a => a.Street)
                .IsUnicode(false)
                .HasMaxLength(Address.StreetMaxLength)
                .IsRequired();

            builder.OwnsOne(a => a.Location, e =>
            {
                e.Property("_coordinate")
                    .HasColumnName("Location")
                    .HasColumnType("geography");
            });
        }
    }
}
