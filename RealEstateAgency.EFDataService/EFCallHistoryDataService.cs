using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFCallHistoryDataService : EFBaseDataService<CallHistory, long>, DataService.ICallHistoryDataService
    {
        public EFCallHistoryDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<CallHistory, long>> GetKey()
        {
            return c => c.HistoryID;
        }
    }
}
