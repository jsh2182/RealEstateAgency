using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class ExceptionLogMap : EntityTypeConfiguration<Models.ExceptionLog>
    {
        public ExceptionLogMap()
        {
            ToTable("ExceptionLog");
            HasKey(l => l.ID);
            Property(l => l.FormName).HasColumnName("FormName").HasMaxLength(200);
            Property(l => l.MethodName).HasColumnName("MethodName").HasMaxLength(200);
            Property(l => l.ExcpetionDesc).HasColumnName("ExcpetionDesc").HasMaxLength(3000);
            Property(l => l.ExceptionDate).HasColumnName("ExceptionDate").IsRequired();
        }

    }
}
