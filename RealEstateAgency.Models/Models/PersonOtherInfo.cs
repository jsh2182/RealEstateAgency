namespace RealEstateAgency.Models
{
    using System;

    public class PersonOtherInfo
    {
        public long InfoID { get; set; }
        public int CreateByID { get; set; }
        public DateTime CreateDate { get; set; }
        public long PersonID { get; set; }
        public string InfoDesc { get; set; }
        public virtual Person Person { get; set; }
        public virtual User User { get; set; }
    }
}
