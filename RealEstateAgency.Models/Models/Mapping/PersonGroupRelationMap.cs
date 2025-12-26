using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class PersonGroupRelationMap:EntityTypeConfiguration<PersonGroupRelation>
    {
        public PersonGroupRelationMap()
        {
            ToTable("PersonGroupRelation");
            HasKey(p => p.RelationID);
            Property(p => p.RelationID).HasColumnName("RelationID").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(p => p.GroupID).HasColumnName("GroupID").IsRequired();
            Property(p => p.PersonID).HasColumnName("PersonID").IsRequired();
        }
    }
}
