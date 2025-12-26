using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class PersonGroupMap:EntityTypeConfiguration<PersonGroup>
    {
        public PersonGroupMap()
        {
            ToTable("PersonGroup");
            HasKey(p => p.GroupID);
            Property(p => p.GroupID).HasColumnName("GroupID").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(p => p.GroupDesc).HasColumnName("GroupDesc").HasMaxLength(100);
            Property(p => p.GroupName).HasColumnName("GroupName").HasMaxLength(100).IsRequired();
            HasMany(p => p.PersonGroupRelations).WithRequired(p => p.PersonGroup).WillCascadeOnDelete(false);

        }
    }
}
