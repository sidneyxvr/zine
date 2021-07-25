using Argon.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Ordering.Infra.Data.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(nameof(OrderItem));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.ProductName)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.ProductImageUrl)
                .IsUnicode(false)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
