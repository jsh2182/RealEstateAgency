using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFPageListDataService : EFBaseDataService<PageList, int>, DataService.IPageListDataService
    {
        public EFPageListDataService(AgencyContext context):base(context)
        {
                
        }
        public override Expression<Func<PageList, int>> GetKey()
        {
            return p => p.PageID;
        }
        public List<ListTreeView> SelectTreeView(int? id)
        {
            List<ListTreeView> ltv = new List<ListTreeView>();


                ltv = All().Where(w => id == null ? w.ParentID == null : w.ParentID == id)
                        .Select(s => new ListTreeView
                        {
                            id = s.PageID,
                            Name = s.PersianName,
                            hasChildren = s.PageListChlidren.Any()
                        }).ToList();


            return ltv;
        }
    }
}
