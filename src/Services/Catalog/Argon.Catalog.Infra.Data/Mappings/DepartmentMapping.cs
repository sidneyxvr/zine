using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class DepartmentMapping : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable(nameof(Department));

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedNever();

            builder.Ignore(d => d.DomainEvents);

            builder.HasQueryFilter(d => !d.IsDeleted);

            builder.Property(d => d.Name)
                .IsUnicode(false)
                .HasMaxLength(Department.NameMaxLength)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsUnicode(false)
                .HasMaxLength(Department.DescriptionMaxLength);
        }
    }
}
