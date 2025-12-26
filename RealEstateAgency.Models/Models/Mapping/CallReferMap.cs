using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models.Mapping
{
    public class CallReferMap:EntityTypeConfiguration<CallRefer>
    {
        public CallReferMap()
        {
            ToTable("CallRefer");
            HasKey(c => c.ReferID);
            Property(c => c.ReferID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ReferBy).HasColumnName("ReferBy").IsRequired();
            Property(c => c.ReferDate).HasColumnName("ReferDate").IsRequired();
            Property(c => c.ReferDesc).HasColumnName("ReferDesc").HasMaxLength(1000);
            Property(c => c.ReferTo).HasColumnName("ReferTo").IsRequired();
            HasRequired(c => c.ReferByUser)
                .WithMany(p => p.CallRefersBy)
                .HasForeignKey(p => p.ReferBy)
                .WillCascadeOnDelete(false);
            HasRequired(c => c.ReferToUser)
                .WithMany(p => p.CallRefersTo)
                .HasForeignKey(p => p.ReferTo)
                .WillCascadeOnDelete(false);

        }
    }
}
