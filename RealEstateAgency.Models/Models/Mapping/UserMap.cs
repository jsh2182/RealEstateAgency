using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(u => u.UserID);
            Property(u => u.UserID).HasColumnName("UserID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            Property(u => u.Password).HasColumnName("Password").HasMaxLength(50).IsRequired();
            Property(u => u.UserName).HasColumnName("UserName").HasMaxLength(100).IsRequired();
            Property(u => u.IsActive).HasColumnName("IsActive").IsRequired();
            Property(u => u.IsAdmin).HasColumnName("IsAdmin").IsRequired();
        }
    }
}
