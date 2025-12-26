using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class PersonOtherInfoMap:EntityTypeConfiguration<PersonOtherInfo>
    {
        public PersonOtherInfoMap()
        {
            ToTable("PersonOtherInfo");
            HasKey(p => p.InfoID);
            Property(p => p.InfoID).HasColumnName("InfoID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CreateByID).HasColumnName("CreateByID").IsRequired();
            Property(p => p.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(p => p.InfoDesc).HasColumnName("InfoDesc").HasMaxLength(1000).IsRequired();
            Property(p => p.PersonID).HasColumnName("PersonID").IsRequired();
            HasRequired(p => p.Person).WithMany(p => p.PersonOtherInfoes).HasForeignKey(p => p.PersonID);
            HasRequired(p => p.User).WithMany(u => u.PersonOtherInfoes).HasForeignKey(u => u.CreateByID);
        }
    }
}
