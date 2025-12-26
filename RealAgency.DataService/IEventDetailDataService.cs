using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.DataService
{
    public interface IEventDetailDataService:IBaseDataService<EventDetail, long>
    {
        IQueryable<EventDetailSchema> Search(long eventID);
    }
}
