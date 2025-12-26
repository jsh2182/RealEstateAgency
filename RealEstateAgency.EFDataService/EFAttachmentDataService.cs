using RealEstateAgency.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFAttachmentDataService : EFBaseDataService<Attachment, long>, DataService.IAttachmentDataService
    {
        public EFAttachmentDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<Attachment, long>> GetKey()
        {
            return a => a.AttachmentID;
        }
        public IQueryable<AttachementSchema> Search(AttachementSchema model)
        {
            var qry = All().Where(q => !q.IsDeleted);
            if (model.ParentID.HasValue)
                qry = qry.Where(q => q.ParentID == model.ParentID);
            if (!string.IsNullOrEmpty(model.AttachmentType))
                qry = qry.Where(q => q.AttachmentType == model.AttachmentType);
            var result = qry.Select(q => new AttachementSchema()
            {
                AttachByName = q.AttachByUser.Name,
                AttachContent = q.AttachContent,
                AttachDate = q.AttachDate,
                AttachedBy = q.AttachedBy,
                AttachmentID = q.AttachmentID,
                AttachmentName = q.AttachmentName,
                AttachmentType = q.AttachmentType,
                DeleteByName = q.DeleteByUser.Name,
                DeletedBy = q.DeletedBy,
                ParentID = q.ParentID
            });
            return result;
        }
    }
}
