using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace RealEstateAgency.Models.Mapping
{
    public class CallHistoryMap:EntityTypeConfiguration<CallHistory>
    {
        public CallHistoryMap()
        {
            HasKey(c => c.HistoryID);
            Property(c => c.HistoryID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("CallHistory");
            Property(c => c.CallDate).HasColumnName("CallDate").IsRequired();
            Property(c => c.CallNumber).HasColumnName("CallNumber").HasMaxLength(20).IsRequired();
            Property(c => c.PersonID).HasColumnName("PersonID");
            Property(c => c.UserID).HasColumnName("UserID");
            HasOptional(c => c.Person).WithMany(p => p.CallHistories).HasForeignKey(c => c.PersonID);
            HasOptional(c => c.User).WithMany(u => u.CallHistories).HasForeignKey(c => c.UserID);
        }
    }
}
