using RealEstateAgency.Models.Schema;
using System.Collections.Generic;

namespace RealEstateAgency.DataService
{
    public interface IPageListDataService:IBaseDataService<RealEstateAgency.Models.PageList, int>
    {
        List<ListTreeView> SelectTreeView(int? id);
    }
}
