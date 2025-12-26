namespace RealEstateAgency.Models
{
    using System;

    public class PageListPermission
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
        public virtual User User { get; set; }
        public virtual PageList PageList { get; set; }
    }
}
