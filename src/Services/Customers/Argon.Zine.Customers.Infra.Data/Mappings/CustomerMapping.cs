using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Utils;
using Argon.Zine.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Zine.Customers.Infra.Data.Mappings;

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
                .IsRequired()
                .HasColumnType($"varchar({Name.MaxLengthFirstName})");

            c.Property(p => p.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasColumnType($"varchar({Name.MaxLengthLastName})");
        });

        builder.OwnsOne(c => c.BirthDate, e =>
        {
            e.Property("_date")
                .IsRequired()
                .HasColumnName("BirthDate")
                .HasColumnType("date");
        });

        builder.Property<DateTime>("CreatedAt")
            .IsRequired()
            .HasColumnType("smalldatetime");

        builder.OwnsOne(c => c.Email, e =>
        {
            e.Property(p => p.Address)
                .HasColumnName("Email")
                .IsRequired()
                .HasColumnType($"varchar({Email.MaxLength})");
        });

        builder.OwnsOne(c => c.Cpf, c =>
        {
            c.Property(p => p.Number)
                .HasColumnName("CPF")
                .IsRequired()
                .HasColumnType($"char({CpfValidator.NumberLength})");
        });

        builder.OwnsOne(c => c.Phone, c =>
        {
            c.Property(p => p.Number)
                .HasColumnName("Phone")
                .IsRequired()
                .HasColumnType($"varchar({Phone.NumberMaxLength})");
        });

        builder.HasOne(c => c.MainAddress)
            .WithOne(m => m.Customer)
            .HasForeignKey<Customer>(c => c.MainAddressId);

        builder.Metadata
            .FindNavigation(nameof(Customer.Addresses))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.Addresses)
            .WithOne()
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}