using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RealEstateAgency.Models.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            ToTable("Person");
            HasKey(P => P.PersonID);
            Property(p => p.PersonID).HasColumnName("PersonID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.BirthDate).HasColumnName("BirthDate");
            Property(p => p.CityID).HasColumnName("CityID");
            Property(p => p.CreateByID).HasColumnName("CreateByID").IsRequired();
            Property(p => p.CreationDate).HasColumnName("CreationDate").IsRequired();
            Property(p => p.CustomerStatusID).HasColumnName("CustomerStatusID");
            Property(p => p.Description).HasColumnName("Description").HasMaxLength(1000);
            Property(p => p.Email).HasColumnName("Email").HasMaxLength(50);
            Property(p => p.Fax).HasColumnName("Fax").HasMaxLength(50);
            Property(p => p.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            Property(p => p.Latitude).HasColumnName("Latitude");
            Property(p => p.Longtitude).HasColumnName("Longtitude");
            Property(p => p.MobileNumber).HasColumnName("MobileNumber").HasMaxLength(100);
            Property(p => p.NationalCode).HasColumnName("NationalCode").HasMaxLength(50);
            Property(p => p.PersonAddressStreet).HasColumnName("PersonAddressStreet").HasMaxLength(300);
            Property(p => p.PersonAddressAlley).HasColumnName("PersonAddressAlley").HasMaxLength(300);
            Property(p => p.PersonAddressNumber).HasColumnName("PersonAddressNumber").HasMaxLength(300);
            Property(p => p.PersonName).HasColumnName("PersonName").HasMaxLength(100).IsRequired();
            Property(p => p.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(100);
            Property(p => p.PostalCode).HasColumnName("PostalCode").HasMaxLength(50);
            Property(p => p.ProvinceID).HasColumnName("ProvinceID").IsRequired();
            Property(p => p.RespectTitle).HasColumnName("RespectTitle").HasMaxLength(100);
            Property(p => p.SubscriptionCode).HasColumnName("SubscriptionCode").HasMaxLength(100);
            Property(p => p.UpdateByID).HasColumnName("UpdateByID");
            Property(p => p.UpdateDate).HasColumnName("UpdateDate");
            Property(p => p.WayOfIntroduce).HasColumnName("WayOfIntroduce").HasMaxLength(100);
            Property(p => p.ZoneID).HasColumnName("ZoneID");
            HasRequired(p => p.Province).WithMany(p => p.People).HasForeignKey(p => p.ProvinceID);
            HasOptional(p => p.City).WithMany(p => p.People).HasForeignKey(p => p.CityID);
            HasOptional(p => p.Zone).WithMany(z => z.People).HasForeignKey(p => p.ZoneID);
            HasRequired(p => p.CreateUser).WithMany(c => c.People).HasForeignKey(p => p.CreateByID);
            HasOptional(p => p.UpdateUser).WithMany(u => u.People1).HasForeignKey(p => p.UpdateByID);
    }
    }
}
