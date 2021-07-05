using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argon.Catalog.Infra.Data.Mappings
{
    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag));

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.Property(t => t.Name)
                .IsUnicode(false)
                .HasMaxLength(Tag.MaxLength)
                .IsRequired();
        }
    }
}
