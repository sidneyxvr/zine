using Argon.Core.Utils;
using Argon.Suppliers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Argon.Suppliers.Infra.Data.Mappings
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

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.CorporateName)
                .IsUnicode(false)
                .HasMaxLength(Supplier.CorporateNameMaxLength);

            builder.Property(s => s.TradeName)
                .IsUnicode(false)
                .HasMaxLength(Supplier.TradeNameMaxLength);

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
                .WithOne(a => a.Supplier)
                .HasForeignKey<Supplier>(s => s.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Users)
                .WithOne(u => u.Supplier)
                .HasForeignKey(u => u.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
