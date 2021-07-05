using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Ignore(c => c.DomainEvents);

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.Property(c => c.Name)
                .IsUnicode(false)
                .HasMaxLength(Category.NameMaxLength)
                .IsRequired(true);

            builder.Property(c => c.Description)
                .IsUnicode(false)
                .HasMaxLength(Category.DescriptionMaxLength);

            builder.HasOne(c => c.Department)
                .WithMany(d => d.Categories)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
