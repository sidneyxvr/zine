using Argon.Zine.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Zine.Ordering.Infra.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever();


            builder.HasOne(o => o.CurrentOrderStatus)
                .WithOne()
                .HasForeignKey<Order>("CurrentOrderStatusId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OrderStatuses)
                .WithOne()
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(a => a.City)
                 .HasColumnType("varchar(40)")
                 .HasColumnName(nameof(Address.City))
                 .IsRequired();

                a.Property(a => a.Complement)
                    .HasColumnType("varchar(50)")
                    .HasColumnName(nameof(Address.Complement))
                    .IsRequired(false);

                a.Property(a => a.Country)
                    .HasColumnType("varchar(50)")
                    .HasColumnName(nameof(Address.Country))
                    .IsRequired();

                a.Property(a => a.District)
                    .HasColumnType("varchar(50)")
                    .HasColumnName(nameof(Address.District))
                    .IsRequired();

                a.Property(a => a.Number)
                    .HasColumnType("varchar(10)")
                    .HasColumnName(nameof(Address.Number));

                a.Property(a => a.PostalCode)
                    .HasColumnType("char(8)")
                    .HasColumnName(nameof(Address.PostalCode))
                    .IsRequired();

                a.Property(a => a.State)
                    .HasColumnType("char(2)")
                    .HasColumnName(nameof(Address.State))
                    .IsRequired();

                a.Property(a => a.Street)
                    .HasColumnType("varchar(50)")
                    .HasColumnName(nameof(Address.Street))
                    .IsRequired();
            });
        }
    }
}
