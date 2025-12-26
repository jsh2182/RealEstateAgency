using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class ProvinceMap:EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            ToTable("Province");
            HasKey(p => p.ProvinceID);
            Property(p => p.ProvinceID).HasColumnName("ProvinceID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.ProvinceName).HasColumnName("ProvinceName").HasMaxLength(40).IsRequired();

        }
    }
}
