using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace RealEstateAgency.EFDataService
{
    public class EFProvinceDataService : EFBaseDataService<Province, int>, DataService.IProvinceDataService
    {
        public EFProvinceDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<Province, int>> GetKey()
        {
            return p => p.ProvinceID;
        }
        public bool IsDeleteValid(int provinceID)
        {
            var result = true;
            result = Context.EstateFiles.Any(e => e.ProvinceID == provinceID);
            if (result)
                return !result;
            result = Context.Cities.Any(c => c.ProvinceID == provinceID);
            if (result)
                return !result;
            result = Context.People.Any(p => p.ProvinceID == provinceID);
            if (result)
                return !result;
            return true;

        }
    }
}
