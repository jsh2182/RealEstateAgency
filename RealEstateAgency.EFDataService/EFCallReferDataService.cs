using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFCallReferDataService : EFBaseDataService<CallRefer, int>, DataService.ICallReferDataService
    {
        public EFCallReferDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<CallRefer, int>> GetKey()
        {
            return c => c.ReferID;
        }
    }
}
