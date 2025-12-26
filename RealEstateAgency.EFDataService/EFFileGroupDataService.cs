using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFFileGroupDataService : EFBaseDataService<FileGroup, long>, DataService.IFileGroupDataService
    {
        public EFFileGroupDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<FileGroup, long>> GetKey()
        {
            return f => f.GroupID;
        }

        public IQueryable<FileGroupSchema> Search(FileGroupSchema model)
        {
            var qry = All();
            if (!string.IsNullOrEmpty(model.GroupDesc))
                qry = qry.Where(q => q.GroupDesc.Contains(model.GroupDesc));
            if (model.GroupID > 0)
                qry = qry.Where(q => q.GroupID == model.GroupID);
            if (model.FileID >= 0)
                qry = qry.Where(q => q.FileGroupRelations.Any(f => f.FileID == model.FileID));
            if (!string.IsNullOrEmpty(model.GroupName))
                qry = qry.Where(q => q.GroupName.Contains(model.GroupName));
            return qry.Select(q => new FileGroupSchema
            {
                GroupDesc = q.GroupDesc,
                GroupID = q.GroupID,
                GroupName = q.GroupName
            });
        }
    }
}
