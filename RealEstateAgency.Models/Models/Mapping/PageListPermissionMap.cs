using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class PageListPermissionMap:EntityTypeConfiguration<PageListPermission>
    {
        public PageListPermissionMap()
        {
            ToTable("PageListPermission","Sec");
            HasKey(p => p.PermissionID);
            Property(p => p.PermissionID).HasColumnName("PermissionID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(p => p.IsDelete).HasColumnName("IsDelete").IsRequired();
            Property(p => p.IsRead).HasColumnName("IsRead").IsRequired();
            Property(p => p.IsSave).HasColumnName("IsSave").IsRequired();
            Property(p => p.IsUpdate).HasColumnName("IsUpdate").IsRequired();
            Property(p => p.PageListID).HasColumnName("PageListID").IsRequired();
            Property(p => p.PermissionID).HasColumnName("PermissionID").IsRequired();
            Property(p => p.PersonID).HasColumnName("PersonID").IsRequired();
            Property(p => p.UserID).HasColumnName("UserID").IsRequired();
            HasRequired(p => p.PageList).WithMany(p => p.PageListPermissions).HasForeignKey(p => p.PageListID);
            HasRequired(p => p.User).WithMany(u => u.PageListPermissions).HasForeignKey(p => p.UserID);
        }
    }
}
