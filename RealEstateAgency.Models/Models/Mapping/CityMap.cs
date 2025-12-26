using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class CityMap:EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            ToTable("City");
            HasKey(c => c.CityID);
            Property(c => c.CityID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.CityName).HasColumnName("CityName").IsRequired().HasMaxLength(60);
            Property(c => c.ProvinceID).HasColumnName("ProvinceID").IsRequired();
            HasRequired(c => c.Province).WithMany(p => p.Cities).HasForeignKey(c => c.ProvinceID);

        }
    }
}
