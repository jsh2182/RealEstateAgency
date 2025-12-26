using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class FileGroupMap:EntityTypeConfiguration<FileGroup>
    {
        public FileGroupMap()
        {
            ToTable("FileGroup");
            HasKey(f => f.GroupID);
            Property(f => f.GroupID).HasColumnName("GroupID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.GroupDesc).HasColumnName("GroupDesc").HasMaxLength(250);
            Property(f => f.GroupName).HasColumnName("GroupName").HasMaxLength(100).IsRequired();
            
        }
    }
}
