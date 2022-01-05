using Argon.Restaurants.Domain;
using Argon.Zine.Commom.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Restaurants.Infra.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Ignore(s => s.DomainEvents);

        builder.OwnsOne(c => c.Email, e =>
        {
            e.Property(p => p.Address)
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.MaxLength})");
        });

        builder.Property<DateTime>("CreatedAt")
            .HasColumnType("smalldatetime");

        builder.OwnsOne(c => c.Name, c =>
        {
            c.Property(p => p.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType($"varchar({Name.MaxLengthFirstName})");

            c.Property(p => p.LastName)
                .HasColumnName("LastName")
                .HasColumnType($"varchar({Name.MaxLengthLastName})");
        });
    }
}