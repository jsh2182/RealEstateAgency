using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace RealEstateAgency.EFDataService
{
    public class EFPageListPermissionDataService : EFBaseDataService<PageListPermission, int>, DataService.IPageListPermissionDataService
    {
        public EFPageListPermissionDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<PageListPermission, int>> GetKey()
        {
            return p => p.PageListID;
        }
        public IQueryable<PageListPermissionSchema> Search(PageListPermissionSchema model)
        {
            var qry = All();
            if (model.UserID > 0)
                qry = qry.Where(q => q.UserID == model.UserID);
            if (model.PageListID > 0)
                qry = qry.Where(q => q.PageListID == model.PageListID);
            var result = qry.Select(q => new PageListPermissionSchema
            {
                CreateDate = q.CreateDate,
                IsDelete = q.IsDelete,
                IsRead = q.IsRead,
                IsSave = q.IsSave,
                IsUpdate = q.IsUpdate,
                PageListID = q.PageListID,
                PermissionID = q.PermissionID,
                PersonID = q.PersonID,
                UserID = q.UserID
            });
            return result;

        }
        public bool UpdateAndSave(PageListPermission plp, int userID, int personID)
        {
            var qry = All().Where(w => w.UserID == plp.UserID && w.PageListID == plp.PageListID).FirstOrDefault();

            if (qry == null)
            {
                Add(plp);
            }
            if (qry != null)
            {
                qry.IsDelete = plp.IsDelete;
                qry.IsRead = plp.IsRead;
                qry.IsSave = plp.IsSave;
                qry.IsUpdate = plp.IsUpdate;
                qry.UserID = plp.UserID;
                Update(qry);
            }

            if (qry != null)
            {
                if (qry.PageList.ParentID != null)
                {
                    var parentPageListPermission = new PageListPermission
                    {
                        CreateDate = DateTime.Now,
                        PageListID = qry.PageList.ParentID.Value,
                        UserID = plp.UserID,
                        IsDelete = plp.IsDelete,
                        IsRead = plp.IsRead,
                        IsSave = plp.IsSave,
                        IsUpdate = plp.IsUpdate
                    };
                    UpdateAndSave(parentPageListPermission, userID, personID);
                }

            }

            return true;
        }
    }
}
