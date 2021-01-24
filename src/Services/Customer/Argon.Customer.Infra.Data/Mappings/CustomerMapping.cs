using Argon.Core.DomainObjects;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Argon.Customers.Infra.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Ignore(c => c.DomainEvents);

            builder.Property(c => c.FullName)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.BirthDate)
                .HasColumnType("date");

            builder.Property<DateTime>("CreatedAt")
                .HasColumnType("smalldatetime");

            builder.OwnsOne(c => c.Email, e =>
            {
                e.Property(p => p.Address)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.OwnsOne(c => c.Cpf, c =>
            {
                c.Property(p => p.Number)
                    .HasColumnName("CPF")
                    .HasColumnType($"char({Cpf.NumberLength})");
            });

            builder.OwnsOne(c => c.Phone, c =>
            {
                c.Property(p => p.Number)
                    .HasColumnName("Phone")
                    .HasColumnType($"varchar({Phone.NumberMaxLength})");
            });

            builder.HasOne(c => c.MainAddress)
                .WithOne()
                .HasForeignKey<Address>(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(Customer.Addresses))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Addresses)
                .WithOne()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
