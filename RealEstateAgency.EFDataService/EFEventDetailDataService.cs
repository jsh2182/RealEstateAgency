using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.EFDataService
{
    public class EFEventDetailDataService : EFBaseDataService<EventDetail, long>, IEventDetailDataService
    {
        public EFEventDetailDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<EventDetail, long>> GetKey()
        {
            return d => d.DetailID;
        }
        public IQueryable<EventDetailSchema> Search(long eventID)
        {
            return All().Where(e => e.EventID == eventID).Select(
                 e => new EventDetailSchema
                 {
                     DetailID = e.DetailID,
                     DetailName = e.DetailName,
                     DetailValue = e.DetailValue,
                     EventID = e.EventID
                 });
        }
    }
}
