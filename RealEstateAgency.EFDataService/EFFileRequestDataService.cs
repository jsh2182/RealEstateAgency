using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFFileRequestDataService : EFBaseDataService<FileRequest, long>, IFileRequestDataService
    {
        public EFFileRequestDataService(AgencyContext agencyContext):base(agencyContext)
        {

        }
        public override Expression<Func<FileRequest, long>> GetKey()
        {
            return r => r.FileRequestID; 
        }
        public IQueryable<FileRequestSchema> Search(FileRequestSchema model)
        {
            var currentUser = Tools.CurrentUser.UserID;
            var qry = from req in All() join file in Context.EstateFiles 
                      on req.FileID equals file.FileID into files
                      from file in files.DefaultIfEmpty()
                      where (req.RequestBy == currentUser || file.CreateByID == currentUser) && (!req.IsRefered)
                      select req;
            if (model.FileID > 0)
                qry = qry.Where(q => q.FileID == model.FileID);
            if (model.FileRequestID > 0)
                qry = qry.Where(q => q.FileRequestID == model.FileRequestID);
            if (model.IsRefered.HasValue)
                qry = qry.Where(q => q.IsRefered == model.IsRefered);
            //if (model.RequestBy > 0)
             //   qry = qry.Where(q => q.RequestBy == model.RequestBy);
            if (!string.IsNullOrEmpty(model.RequestDesc))
                qry = qry.Where(q => q.RequestDesc.Contains(model.RequestDesc));
            var result = qry.Select(q => new FileRequestSchema()
            {
                FileID = q.FileID,
                FileCode = q.File.FileCode,
                FileRequestID = q.FileRequestID,
                IsRefered = q.IsRefered,
                RequestBy = q.RequestBy,
                RequestByName = q.RequestByUser.Name,
                RequestDate = q.RequestDate,
                RequestDesc = q.RequestDesc,
                RequestType = q.RequestBy == currentUser?"درخواست من":"درخواست دیگران"
            });
            return result;
        }
        private FileRequest InitRequest(FileRequestSchema model)
        {
            FileRequest request;
            if (model.FileRequestID > 0)
                request = Get(model.FileRequestID);
            else
            {
                request = new FileRequest();
                request.RequestBy = Tools.CurrentUser.UserID;
                request.RequestDate = DateTime.Now;
            }
            request.FileID = model.FileID;
            request.IsRefered = model.IsRefered.HasValue;
            request.RequestDesc = model.RequestDesc;
            return request;
        }
        public FileRequest UpdateRequest(FileRequestSchema model)
        {
            var req = InitRequest(model);
            req = Update(req);
            return req;
        }
        public FileRequest AddRequest(FileRequestSchema model)
        {
            var req = InitRequest(model);
            req = Add(req);
            return req;
        }
        public Enums.RemoveResult RemoveRequest(long id)
        {
            
            var req = Get(id);
            if (req == null || req.FileRequestID == 0)
                return Enums.RemoveResult.NotFoundForRemove;
            if (Context.FileRefers.Any(r => r.RequestID == id))
                return Enums.RemoveResult.DeleteIsNotValid;
            Delete(req);
            return Enums.RemoveResult.Success;
        }
        public bool RequestExists(int userID, long fileID)
        {
            return All().Any(r => r.RequestBy == userID && r.FileID == fileID);
        }
    }
}
