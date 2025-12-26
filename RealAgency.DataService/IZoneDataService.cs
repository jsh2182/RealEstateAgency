using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.DataService
{
    public interface IZoneDataService : IBaseDataService<RealEstateAgency.Models.Zone, int>
    {
        bool IsDeleteValid(int id);
        IQueryable<Models.Schema.ZoneSchema> Search(int cityID);
    }
}
