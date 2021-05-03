using Argon.Core.DomainObjects;
using Argon.Core.Utils;
using Argon.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Argon.Customers.Infra.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedNever();

            builder.Ignore(c => c.DomainEvents);

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.OwnsOne(c => c.Name, c =>
            {
                c.Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .HasColumnType($"varchar({Name.MaxLengthFirstName})");

                c.Property(p => p.Surname)
                    .HasColumnName("Surname")
                    .HasColumnType($"varchar({Name.MaxLengthSurname})");
            });

            builder.OwnsOne(c => c.BirthDate, e =>
            {
                e.Property("_date")
                    .HasColumnName("BirthDate")
                    .HasColumnType("date");
            });

            builder.Property<DateTime>("CreatedAt")
                .HasColumnType("smalldatetime");

            builder.OwnsOne(c => c.Email, e =>
            {
                e.Property(p => p.Address)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.MaxLength})");
            });

            builder.OwnsOne(c => c.Cpf, c =>
            {
                c.Property(p => p.Number)
                    .HasColumnName("CPF")
                    .HasColumnType($"char({CpfValidator.NumberLength})");
            });

            builder.OwnsOne(c => c.Phone, c =>
            {
                c.Property(p => p.Number)
                    .HasColumnName("Phone")
                    .HasColumnType($"varchar({Phone.NumberMaxLength})");
            });

            builder.HasOne(c => c.MainAddress)
                .WithOne()
                .HasForeignKey<Customer>("MainAddressId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.Metadata
            //    .FindNavigation(nameof(Customer.Addresses))
            //    .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Addresses)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
