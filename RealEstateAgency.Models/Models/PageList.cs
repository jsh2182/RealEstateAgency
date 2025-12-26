namespace RealEstateAgency.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PageList
    {
        public PageList()
        {
            PageListChlidren = new List<PageList>();
            PageListPermissions = new List<PageListPermission>();
        }

         public int PageID { get; set; }
        public string Name { get; set; }

        public int? ParentID { get; set; }
        public string Description { get; set; }
        public string PersianName { get; set; }

        public string Url { get; set; }
        public virtual ICollection<PageList> PageListChlidren { get; set; }

        public virtual PageList PageList2 { get; set; }
        public virtual ICollection<PageListPermission> PageListPermissions { get; set; }
    }
}
