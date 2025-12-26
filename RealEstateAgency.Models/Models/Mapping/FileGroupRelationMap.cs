using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class FileGroupRelationMap:EntityTypeConfiguration<FileGroupRelation>
    {
        public FileGroupRelationMap()
        {
            ToTable("FileGroupRelation");
            HasKey(f => f.RelationID);
            Property(f => f.RelationID).HasColumnName("RelationID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.GroupID).HasColumnName("GroupID").IsRequired();
            Property(f => f.FileID).HasColumnName("FileID").IsRequired();
            HasRequired(r => r.EstateFile)
                .WithMany(e => e.FileGroupRelations)
                .HasForeignKey(r => r.FileID);
            HasRequired(r => r.FileGroup)
                .WithMany(e => e.FileGroupRelations)
                .HasForeignKey(r => r.GroupID);
        }
    }
}
