using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Zine.Ordering.Infra.Data.Mappings;

public class BuyerMapping : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable(nameof(Buyer));

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(b => b.Name, c =>
        {
            c.Property(p => p.FirstName)
                .HasColumnName("FirstName")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(Name.MaxLengthFirstName);

            c.Property(p => p.Surname)
                .HasColumnName("Surname")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(Name.MaxLengthSurname);
        });

        builder.HasMany(b => b.PaymentMethods)
            .WithOne()
            .HasForeignKey("BuyerId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}