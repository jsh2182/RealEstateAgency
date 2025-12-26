using System.Data.Entity.ModelConfiguration;


namespace RealEstateAgency.Models.Mapping
{
    public class AttachmentMap : EntityTypeConfiguration<Attachment>
    {
        public AttachmentMap()
        {
            HasKey(a => a.AttachmentID);
            Property(a => a.AttachmentID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(a => a.AttachContent).HasColumnName("AttachContent");
            Property(a => a.AttachDate).HasColumnName("AttachDate").IsRequired();
            Property(a => a.AttachedBy).HasColumnName("AttachedBy").IsRequired();
            Property(a => a.AttachmentName).HasColumnName("AttachmentName").HasMaxLength(250).IsRequired();
            Property(a => a.AttachmentType).HasColumnName("AttachmentType").HasMaxLength(50);
            Property(a => a.DeletedBy).HasColumnName("DeletedBy");
            Property(a => a.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            Property(a => a.ParentID).HasColumnName("ParentID");
            HasRequired(e => e.AttachByUser).WithMany(p => p.Attachments)
            .HasForeignKey(e => e.AttachedBy)
            .WillCascadeOnDelete(false);
            HasOptional(a => a.DeleteByUser).WithMany(p => p.Attachments_Delete)
                            .HasForeignKey(e => e.DeletedBy);
        }
    }
}
