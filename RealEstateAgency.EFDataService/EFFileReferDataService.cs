using RealEstateAgency.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFFileReferDataService : EFBaseDataService<FileRefer, long>, DataService.IFileReferDataService
    {
        public EFFileReferDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<FileRefer, long>> GetKey()
        {
            return f => f.FileReferID;
        }
        public IQueryable<FileReferSchema> Search(FileReferSchema model)
        {
            var qry = All();
            if (model.FileReferID > 0)
                qry = qry.Where(q => q.FileReferID == model.FileReferID);
            if (model.FileID > 0)
                qry = qry.Where(q => q.FileID == model.FileID);
            if (!string.IsNullOrEmpty(model.ReferDesc))
                qry = qry.Where(q => q.ReferDesc.Contains(model.ReferDesc));
            if (model.ReferedBy > 0)
                qry = qry.Where(q => q.ReferedBy == model.ReferedBy);
            if (model.ReferedTo > 0)
                qry = qry.Where(q => q.ReferedTo == model.ReferedTo);
            var result = qry.Select(q => new FileReferSchema
            {
                FileReferID = q.FileReferID,
                FileID = q.FileID,
                ReferByName = q.User_ReferBy.Name,
                ReferDate = q.ReferDate,
                ReferDesc = q.ReferDesc,
                ReferedBy = q.ReferedBy,
                ReferedTo = q.ReferedTo,
                ReferToName = q.User_ReferTo.Name
            });
            return result;
        }
        public FileRefer AddRefer(FileReferSchema model)
        {
            var refer = new FileRefer
            {
                FileID = model.FileID,
                ReferDate = DateTime.Now,
                ReferDesc = model.ReferDesc,
                ReferedBy = Tools.CurrentUser.UserID,
                ReferedTo = model.ReferedTo
            };
            refer = Add(refer);
            var request = Context.FileRequests.FirstOrDefault(r => r.FileID == model.FileID && r.RequestBy == model.ReferedTo);
            if(request != null && request.FileRequestID > 0)
            {
                request.IsRefered = true;
                Context.SaveChanges();
            }
            return refer;
        }
        public FileRefer UpdateRefer(FileReferSchema model)
        {
            var refer = Get(model.FileReferID);
            refer.ReferDesc = model.ReferDesc;
            refer.ReferedTo = model.ReferedTo;
            refer = Update(refer);
            return refer;
        }
        public void DeleteRefer(long referID)
        {
            var refer = Get(referID);
            Delete(refer);
        }
    }
}
