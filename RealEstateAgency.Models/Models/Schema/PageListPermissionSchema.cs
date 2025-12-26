using System;

namespace RealEstateAgency.Models.Schema
{
    public class PageListPermissionSchema
    {
        public int PermissionID { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public int PageListID { get; set; }
        public bool IsRead { get; set; }
        public bool IsSave { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
