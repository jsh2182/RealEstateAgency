using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IPageListPermissionDataService:IBaseDataService<Models.PageListPermission, int>
    {
        IQueryable<PageListPermissionSchema> Search(PageListPermissionSchema model);
        bool UpdateAndSave(PageListPermission plp, int userID, int personID);
    }
}
