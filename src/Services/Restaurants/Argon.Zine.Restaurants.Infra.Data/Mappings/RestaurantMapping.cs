using Argon.Restaurants.Domain;
using Argon.Zine.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Restaurants.Infra.Data.Mappings;

public class SupplierMapping : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable(nameof(Restaurant));

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Ignore(s => s.DomainEvents);

        builder.HasQueryFilter(s => !s.IsDeleted);

        builder.Property(s => s.CorporateName)
            .IsUnicode(false)
            .HasMaxLength(Restaurant.CorporateNameMaxLength);

        builder.Property(s => s.TradeName)
            .IsUnicode(false)
            .HasMaxLength(Restaurant.TradeNameMaxLength);

        builder.Property(s => s.LogoUrl)
            .IsUnicode(false)
            .HasMaxLength(256);

        builder.Property<DateTime>("CreatedAt")
            .HasColumnType("smalldatetime");

        builder.OwnsOne(s => s.CpfCnpj, c =>
        {
            c.Property(p => p.Number)
                .HasColumnName("CPFCNPJ")
                .IsUnicode(false)
                .HasMaxLength(CnpjValidator.NumberLength)
                .IsRequired();
        });

        builder.HasOne(s => s.Address)
            .WithOne(a => a.Restaurant)
            .HasForeignKey<Restaurant>(s => s.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Users)
            .WithOne(u => u.Restaurant)
            .HasForeignKey(u => u.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}