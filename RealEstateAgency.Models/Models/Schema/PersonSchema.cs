using RealEstateAgency.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Schema
{
    public class PersonSchema
    {
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
        public string ProvinceName { get; set; }
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public int? ZoneID { get; set; }
        public string ZoneName { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PBirthDate { get { return BirthDate != null ? DateHelper.ToPersian((DateTime)BirthDate) : ""; } }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string RespectTitle { get; set; }
        public int? CustomerStatusID { get; set; }
        public string Description { get; set; }
        public string SubscriptionCode { get; set; }
        public string WayOfIntroduce { get; set; }
        public DateTime CreationDate { get; set; }
        public string PCreationDate { get { return DateHelper.ToPersian(CreationDate); } }
        public int CreateByID { get; set; }
        public string CreateByName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string PUpdateDate { get { return UpdateDate != null ? DateHelper.ToPersian((DateTime)UpdateDate) : ""; } }
        public int? UpdateByID { get; set; }
        public string UpdateByName { get; set; }
    }
}
