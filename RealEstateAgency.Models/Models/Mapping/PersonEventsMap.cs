using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class PersonEventsMap : EntityTypeConfiguration<PersonEvents>
    {
        public PersonEventsMap()
        {
            ToTable("PersonEvents");
            HasKey(e => e.EventID);
            Property(e => e.EventID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.AttachmentID).HasColumnName("AttachmentID");
            Property(e => e.AttachmentType).HasColumnName("AttachmentType").HasMaxLength(50);
            Property(e => e.Consultant).HasColumnName("Consultant");
            Property(e => e.CreateByID).HasColumnName("CreateByID").IsRequired();
            Property(e => e.CreationDate).HasColumnName("CreationDate").IsRequired();
            Property(e => e.Description).HasColumnName("Description").HasMaxLength(1000).IsRequired();
            Property(e => e.PersonID).HasColumnName("PersonID");
            Property(e => e.EventType).HasColumnName("EventType").HasMaxLength(20).IsRequired();
            HasOptional(e => e.Person).WithMany(p => p.PersonEvents).HasForeignKey(e => e.PersonID);
            HasRequired(e => e.CreateUser).WithMany(p => p.Events_CreateUser).HasForeignKey(e => e.CreateByID);
            HasOptional(e => e.UpdateUser).WithMany(p => p.Events_UpdateUser).HasForeignKey(e => e.UpdateByID);
            HasOptional(e => e.ConsultantUser).WithMany(p => p.Events_ConsultantUser).HasForeignKey(e => e.Consultant);
        }
    }
}
