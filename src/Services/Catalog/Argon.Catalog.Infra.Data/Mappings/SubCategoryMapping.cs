using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class SubCategoryMapping : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable(nameof(SubCategory));

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Ignore(s => s.DomainEvents);

            builder.HasQueryFilter(s => !s.IsDeleted);

            builder.Property(s => s.Name)
                .IsUnicode(false)
                .HasMaxLength(SubCategory.NameMaxLength)
                .IsRequired();

            builder.Property(s => s.Description)
                .IsUnicode(false)
                .HasMaxLength(SubCategory.DescriptionMaxLength);

            builder.HasOne(c => c.Category)
                .WithMany(s => s.SubCategories)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
