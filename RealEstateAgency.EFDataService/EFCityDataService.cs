using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;
using System.Linq;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFCityDataService : EFBaseDataService<City, int>, DataService.ICityDataService
    {
        public EFCityDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<City, int>> GetKey()
        {
            return c => c.CityID;
        }
        public bool IsDeleteValid(int id)
        {
            
            var result = Context.EstateFiles.Any(e => e.CityID == id);
            if (result)
                return false; ;
            result = Context.People.Any(p => p.CityID == id);
            if (result)
                return false;
            result = Context.Zones.Any(z => z.CityID == id);
            if (result)
                return false;
            return true;
        }

        public IQueryable<CitySchema> Search(int provinceID)
        {
            var qry = All().Where(c => c.ProvinceID == provinceID)
                            .Select(c=> new CitySchema {
                                CityID = c.CityID,
                                CityName = c.CityName,
                                ProvinceID = c.ProvinceID,
                                ProvinceName = c.Province.ProvinceName
            });
            return qry;
        }
    }
}
