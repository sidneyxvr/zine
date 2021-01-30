using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite;

namespace Argon.Customers.Infra.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.City)
                .HasColumnType("varchar(40)")
                .IsRequired();

            builder.Property(a => a.Complement)
                .HasColumnType("varchar(50)");

            builder.Property(a => a.Country)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(a => a.District)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(a => a.Number)
                .HasColumnType("varchar(5)");

            builder.Property(a => a.PostalCode)
                .HasColumnType("char(8)")
                .IsRequired();

            builder.Property(a => a.State)
                .HasColumnType("char(2)")
                .IsRequired();

            builder.Property(a => a.Street)
                .HasColumnType("varchar(50)")
                .IsRequired();

            NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            builder.OwnsOne(a => a.Location, e =>
            {
                e.Property("_coordinate")
                    .HasColumnName("Location")
                    .HasColumnType("geography (point)");
            });
        }
    }
}
