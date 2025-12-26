namespace RealEstateAgency.Models
{
    using System;
    public partial class CallRefer
    {
        public int ReferID { get; set; }
        public int ReferTo { get; set; }
        public int ReferBy { get; set; }
        public DateTime ReferDate { get; set; }
        public string ReferDesc { get; set; }
        public virtual User ReferByUser { get; set; }
        public virtual User ReferToUser { get; set; }
    }
}
