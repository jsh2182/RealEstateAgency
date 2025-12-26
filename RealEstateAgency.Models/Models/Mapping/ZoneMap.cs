using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class ZoneMap:EntityTypeConfiguration<Zone>
    {
        public ZoneMap()
        {
            ToTable("Zone");
            HasKey(z => z.ZoneID);
            Property(z => z.ZoneID).HasColumnName("ZoneID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(z => z.ZoneName).HasColumnName("ZoneName").HasMaxLength(150).IsRequired();
            Property(z => z.CityID).HasColumnName("CityID").IsRequired();
            HasRequired(z => z.City).WithMany(c => c.Zones).HasForeignKey(z => z.CityID);
        }
    }
}
