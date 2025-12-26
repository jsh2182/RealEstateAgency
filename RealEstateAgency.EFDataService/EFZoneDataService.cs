using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace RealEstateAgency.EFDataService
{
    public class EFZoneDataService : EFBaseDataService<Zone, int>, DataService.IZoneDataService
    {
        public EFZoneDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<Zone, int>> GetKey()
        {
            return z => z.ZoneID;
        }
        public bool IsDeleteValid(int id)
        {

            var result = Context.EstateFiles.Any(e => e.ZoneID == id);
            if (result)
                return false;
            result = Context.People.Any(p => p.ZoneID == id);
            if (result)
                return false;
            return true;
        }

        public IQueryable<Models.Schema.ZoneSchema> Search(int cityID)
        {
            var qry = All().Where(z => z.CityID == cityID)
                .Select(z => new Models.Schema.ZoneSchema { ZoneID = z.ZoneID, ZoneName = z.ZoneName });
            return qry;
        }

    }
}
