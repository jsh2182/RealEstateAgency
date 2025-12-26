using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class FileRequestMap:EntityTypeConfiguration<FileRequest>
    {
        public FileRequestMap()
        {
            ToTable("FileRequest");
            HasKey(r => r.FileRequestID);
            Property(r => r.FileID).HasColumnName("FileID").IsRequired();
            Property(r => r.FileRequestID).HasColumnName("FileRequestID");
            Property(r => r.RequestBy).HasColumnName("RequestBy").IsRequired();
            Property(r => r.RequestDate).HasColumnName("RequestDate").IsRequired();
            Property(r => r.RequestDesc).HasColumnName("RequestDesc").HasMaxLength(100);
            Property(r => r.IsRefered).HasColumnName("IsRefered").IsRequired();
            HasRequired(r => r.RequestByUser).WithMany(r => r.FileRequests).HasForeignKey(r => r.RequestBy);
            HasRequired(r => r.File).WithMany(f => f.FileRequests).HasForeignKey(r => r.FileID);
            
        }
    }
}
