using Argon.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Ordering.Infra.Data.Mappings
{
    public class PaymentMethodMapping : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable(nameof(PaymentMethod));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Alias)
                .IsUnicode(false)
                .HasMaxLength(PaymentMethod.AliasMaxLength)
                .IsRequired();

            builder.Property(o => o.CardNamber)
                .HasColumnType($"char({PaymentMethod.CardNumberLength})")
                .IsRequired();

            builder.Property(o => o.CardHolderName)
                .IsUnicode(false)
                .HasMaxLength(PaymentMethod.CardHolderNameMaxLength)
                .IsRequired();

            builder.Property(o => o.Alias)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
