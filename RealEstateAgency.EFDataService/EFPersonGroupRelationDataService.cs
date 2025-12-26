using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFPersonGroupRelationDataService : EFBaseDataService<PersonGroupRelation, long>, DataService.IPersonGroupRelationDataService
    {
        public EFPersonGroupRelationDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<PersonGroupRelation, long>> GetKey()
        {
            return p => p.RelationID;
        }
    }
}
