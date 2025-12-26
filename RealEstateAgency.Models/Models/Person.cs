namespace RealEstateAgency.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Person
    {
        public Person()
        {
            PersonEvents = new List<PersonEvents>();
            PersonGroupRelations = new List<PersonGroupRelation>();
            PersonOtherInfoes = new List<PersonOtherInfo>();
            CallHistories = new List<CallHistory>();
            EstateFiles = new List<EstateFile>();
        }

        public long PersonID { get; set; }
        public string PersonName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string PersonAddressStreet { get; set; }
        public string PersonAddressAlley { get; set; }
        public string PersonAddressNumber { get; set; }
        public string PostalCode { get; set; }
        public int ProvinceID { get; set; }
        public int? CityID { get; set; }
        public int? ZoneID { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string RespectTitle { get; set; }
        public int? CustomerStatusID { get; set; }
        public string Description { get; set; }
        public string SubscriptionCode { get; set; }
        public string WayOfIntroduce { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreateByID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByID { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<PersonEvents> PersonEvents { get; set; }
        public virtual ICollection<CallHistory> CallHistories { get; set; }
        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<PersonGroupRelation> PersonGroupRelations { get; set; }
        public virtual ICollection<PersonOtherInfo> PersonOtherInfoes { get; set; }
        public virtual User UpdateUser { get; set; }
        public virtual User CreateUser { get; set; }
        public virtual ICollection<EstateFile> EstateFiles { get; set; }

    }
}
