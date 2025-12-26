using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Mapping
{
    public class FollowUpEventsMap: EntityTypeConfiguration<FollowUpEvents>
    {
        public FollowUpEventsMap()
        {
            ToTable("FollowUpEvents");
            HasKey(f => f.FollowUpID);
            Property(f => f.FollowUpID).HasColumnName("FollowUpID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.FollowDate).HasColumnName("FollowDate").IsRequired();
            Property(f => f.FollowUpDesc).HasColumnName("FollowUpDesc").HasMaxLength(250);
            Property(f => f.FollowUpUserID).HasColumnName("FollowUpUserID");
            Property(f => f.IsDone).HasColumnName("IsDone").IsRequired();
            Property(f => f.PersonEventID).HasColumnName("PersonEventID").IsRequired();
            Property(f => f.FollowUpCode).HasColumnName("FollowUpCode").HasPrecision(3, 0).IsRequired();
            Property(f => f.FollowUpResult).HasColumnName("FollowUpResult").HasMaxLength(250);
            HasRequired(f => f.PersonEvent).WithMany(p => p.FollowUpEvents).HasForeignKey(f => f.PersonEventID).WillCascadeOnDelete(true);
            HasOptional(f => f.FollowUpUser).WithMany(u => u.FollowUpEvents).HasForeignKey(f => f.FollowUpUserID);

        }
    }
}
