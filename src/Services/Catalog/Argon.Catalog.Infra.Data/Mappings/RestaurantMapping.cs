using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class RestaurantMapping : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable(nameof(Restaurant));

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Ignore(s => s.DomainEvents);

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.Name)
                .IsUnicode(false)
                .HasMaxLength(Restaurant.NameMaxLength)
                .IsRequired(true);

            builder.Property(s => s.Address)
                .IsUnicode(false)
                .HasMaxLength(Restaurant.AddressMaxLength)
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
