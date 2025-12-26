using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models.Mapping
{
    public class PageListMap:EntityTypeConfiguration<PageList>
    {
        public PageListMap()
        {
            ToTable("PageList", "Sec");
            HasKey(p => p.PageID);
            Property(p => p.PageID).HasColumnName("PageID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Description).HasColumnName("Description").HasMaxLength(400);
            Property(p => p.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            Property(p => p.ParentID).HasColumnName("ParentID");
            Property(p => p.PersianName).HasColumnName("PersianName").HasMaxLength(100);
            Property(p => p.Url).HasColumnName("Url").HasMaxLength(250);
            HasMany(p => p.PageListChlidren).WithOptional(p => p.PageList2).HasForeignKey(p => p.ParentID);
        }
    }
}
