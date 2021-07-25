using Argon.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Ordering.Infra.Data.Mappings
{
    public class RestaurantMapping : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable(nameof(Restaurant));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Name)
                .IsUnicode(false)
                .HasMaxLength(Restaurant.NameMaxLength)
                .IsRequired();

            builder.Property(o => o.LogoUrl)
                .IsUnicode(false)
                .HasMaxLength(256);
        }
    }
}
