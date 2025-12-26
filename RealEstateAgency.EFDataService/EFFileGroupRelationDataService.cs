using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFFileGroupRelationDataService : EFBaseDataService<FileGroupRelation, long>, DataService.IFileGroupRelationDataService
    {
        public EFFileGroupRelationDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<FileGroupRelation, long>> GetKey()
        {
            return f => f.RelationID;
        }
    }
}
