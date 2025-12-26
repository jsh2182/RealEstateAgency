using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace RealEstateAgency.EFDataService
{
    public class EFPersonDataService : EFBaseDataService<Person, long>, DataService.IPersonDataService
    {
        public EFPersonDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<Person, long>> GetKey()
        {
            return p => p.PersonID;
        }
        private Person InitPerson(PersonSchema model)
        {
            Person p;
            if (model.PersonID > 0)
                p = Get(model.PersonID);
            else
                p = new Person();
            p.BirthDate = model.BirthDate;
            p.CityID = model.CityID;
            if (model.PersonID == 0)
            {
                p.CreateByID = Tools.CurrentUser.UserID;
                p.IsDeleted = false;
                p.CreationDate = DateTime.Now;
            }
            else
            {
                p.UpdateByID = Tools.CurrentUser.UserID;
                p.UpdateDate = DateTime.Now;
            }
            p.CustomerStatusID = model.CustomerStatusID;
            p.Description = model.Description;
            p.Email = model.Email;
            p.Latitude = model.Latitude;
            p.Longtitude = model.Longtitude;
            p.MobileNumber = model.MobileNumber;
            p.NationalCode = model.NationalCode;
            p.PersonAddressStreet = model.PersonAddressStreet;
            p.PersonAddressAlley = model.PersonAddressAlley;
            p.PersonAddressNumber = model.PersonAddressNumber;
            p.PersonName = model.PersonName;
            p.PhoneNumber = model.PhoneNumber;
            p.PostalCode = model.PostalCode;
            p.ProvinceID = model.ProvinceID;
            p.RespectTitle = model.RespectTitle;
            if (model.SubscriptionCode.Replace(" ", "") == "" || model.SubscriptionCode.Replace(" ", "") == "0")
                p.SubscriptionCode = GetNextCode();
            else
                p.SubscriptionCode = model.SubscriptionCode;
            p.WayOfIntroduce = model.WayOfIntroduce;
            p.ZoneID = model.ZoneID;
            return p;


        }
        public Person UpdatePerson(PersonSchema model)
        {
            var p = InitPerson(model);
            Update(p);
            return p;
        }
        public Person AddPerson(PersonSchema model)
        {
            var p = InitPerson(model);
            Add(p);
            return p;
        }
        public bool IsDeleteNotValid(long id)
        {
            return Context.EstateFiles.Any(p => p.PersonID == id);
        }
        public IQueryable<PersonSchema> Search(PersonSchema model)
        {
            var qry = All();
            if (model.BirthDate.HasValue)
                qry = qry.Where(q => q.BirthDate == model.BirthDate);
            if (model.CityID.HasValue)
                qry = qry.Where(q => q.CityID == model.CityID);
            if (model.CreateByID > 0)
                qry = qry.Where(q => q.CreateByID == model.CreateByID);
            if (model.CreationDate > DateTime.MinValue)
                qry = qry.Where(q => q.CreationDate == model.CreationDate);
            if (model.CustomerStatusID.HasValue)
                qry = qry.Where(q => q.CustomerStatusID == model.CustomerStatusID);
            if (!string.IsNullOrEmpty(model.Description))
                qry = qry.Where(q => q.Description.Contains(model.Description));
            if (!string.IsNullOrEmpty(model.Email))
                qry = qry.Where(q => q.Email.Contains(model.Email));
            if (!string.IsNullOrEmpty(model.Fax))
                qry = qry.Where(q => q.Fax.Contains(model.Fax));
            if (model.Latitude > 0)
                qry = qry.Where(q => q.Latitude == model.Latitude);
            if (model.Longtitude > 0)
                qry = qry.Where(q => q.Longtitude == model.Longtitude);
            if (!string.IsNullOrEmpty(model.MobileNumber))
                qry = qry.Where(q => q.MobileNumber.Contains(model.MobileNumber));
            if (!string.IsNullOrEmpty(model.NationalCode))
                qry = qry.Where(q => q.NationalCode.Contains(model.NationalCode));
            if (!string.IsNullOrEmpty(model.PersonAddressStreet))
                qry = qry.Where(q => q.PersonAddressStreet.Contains(model.PersonAddressStreet));
            if (!string.IsNullOrEmpty(model.PersonAddressAlley))
                qry = qry.Where(q => q.PersonAddressAlley.Contains(model.PersonAddressAlley));
            if (!string.IsNullOrEmpty(model.PersonAddressNumber))
                qry = qry.Where(q => q.PersonAddressNumber.Equals(model.PersonAddressNumber));
            if (!string.IsNullOrEmpty(model.PersonName))
                qry = qry.Where(q => q.PersonName.Contains(model.PersonName));
            if (!string.IsNullOrEmpty(model.PhoneNumber))
                qry = qry.Where(q => q.PhoneNumber.Contains(model.PhoneNumber));
            if (!string.IsNullOrEmpty(model.PostalCode))
                qry = qry.Where(q => q.PostalCode.Contains(model.PostalCode));
            if (model.ProvinceID > 0)
                qry = qry.Where(q => q.ProvinceID == model.ProvinceID);
            if (!string.IsNullOrEmpty(model.RespectTitle))
                qry = qry.Where(q => q.RespectTitle == model.RespectTitle);
            if (!string.IsNullOrEmpty(model.SubscriptionCode))
                qry = qry.Where(q => q.SubscriptionCode.Contains(model.SubscriptionCode));
            if (model.UpdateByID.HasValue)
                qry = qry.Where(q => q.UpdateByID == model.UpdateByID);
            if (model.UpdateDate.HasValue)
                qry = qry.Where(q => q.UpdateDate == model.UpdateDate);
            if (!string.IsNullOrEmpty(model.WayOfIntroduce))
                qry = qry.Where(q => q.WayOfIntroduce.Contains(model.WayOfIntroduce));
            if (model.ZoneID.HasValue)
                qry = qry.Where(q => q.ZoneID == model.ZoneID);
            return qry.Select(q => new PersonSchema
            {
                BirthDate = q.BirthDate,
                CityID = q.CityID,
                CityName = q.City.CityName,
                CreateByID = q.CreateByID,
                CreateByName = q.CreateUser.Name,
                CreationDate = q.CreationDate,
                CustomerStatusID = q.CustomerStatusID,
                Description = q.Description,
                Email = q.Email,
                Fax = q.Fax,
                Latitude = q.Latitude,
                Longtitude = q.Longtitude,
                MobileNumber = q.MobileNumber,
                NationalCode = q.NationalCode,
                PersonAddressStreet = q.PersonAddressStreet,
                PersonAddressAlley = q.PersonAddressAlley,
                PersonAddressNumber = q.PersonAddressNumber,
                PersonID = q.PersonID,
                PersonName = q.PersonName,
                PhoneNumber = q.PhoneNumber,
                PostalCode = q.PostalCode,
                ProvinceID = q.ProvinceID,
                ProvinceName = q.Province.ProvinceName,
                RespectTitle = q.RespectTitle,
                SubscriptionCode = q.SubscriptionCode,
                UpdateByID = q.UpdateByID,
                UpdateByName = q.UpdateUser.Name,
                UpdateDate = q.UpdateDate,
                WayOfIntroduce = q.WayOfIntroduce,
                ZoneID = q.ZoneID,
                ZoneName = q.Zone.ZoneName
            });




        }
        public string GetNextCode()
        {
            var maxCode = All().Select(f => f.SubscriptionCode).ToList().Max(e => long.Parse(e)) + 1;
            var strCode = "000000" + (maxCode).ToString();
            return strCode.Substring(strCode.Length - 7, 7);
        }

    }
}
