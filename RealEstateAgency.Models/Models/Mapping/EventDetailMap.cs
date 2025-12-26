using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class EventDetailMap:EntityTypeConfiguration<EventDetail>
    {
        public EventDetailMap()
        {
            ToTable("EventDetail");
            HasKey(e => e.DetailID);
            Property(e => e.DetailID).HasColumnName("DetailID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.DetailName).HasColumnName("DetailName").HasMaxLength(100).IsRequired();
            Property(e => e.DetailValue).HasColumnName("DetailValue").HasMaxLength(100).IsRequired();
            Property(e => e.EventID).HasColumnName("EventID").IsRequired();
            HasRequired(e => e.Event).WithMany(e => e.Details).HasForeignKey(e => e.EventID);
        }
    }
}
