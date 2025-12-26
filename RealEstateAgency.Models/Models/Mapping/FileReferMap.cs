using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models.Mapping
{
    public class FileReferMap:EntityTypeConfiguration<FileRefer>
    {
        public FileReferMap()
        {
            ToTable("FileRefer");
            HasKey(f => f.FileReferID);
            Property(f => f.FileReferID).HasColumnName("FileReferID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.ReferDate).HasColumnName("ReferDate").IsRequired();
            Property(f => f.ReferDesc).HasColumnName("ReferDesc").HasMaxLength(1000);
            Property(f => f.ReferedBy).HasColumnName("ReferedBy").IsRequired();
            Property(f => f.ReferedTo).HasColumnName("ReferedTo").IsRequired();
            Property(f => f.RequestID).HasColumnName("RequestID");
            HasRequired(f => f.User_ReferBy).WithMany(p => p.FileRefers).HasForeignKey(f => f.ReferedBy);
            HasRequired(f => f.User_ReferTo).WithMany(p => p.FileRefers_ReferTo).HasForeignKey(f => f.ReferedTo);
            HasRequired(F => F.File).WithMany(F => F.FileRefers).HasForeignKey(F => F.FileID);
            HasOptional(f => f.Request).WithMany(f => f.Refers).HasForeignKey(r => r.RequestID);
        }
    }
}
