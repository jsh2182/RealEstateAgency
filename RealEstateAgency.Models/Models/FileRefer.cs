namespace RealEstateAgency.Models
{
    using System;

    public partial class FileRefer
    {
        public long FileReferID { get; set; }
        public long FileID { get; set; }
        public int ReferedBy { get; set; }
        public int ReferedTo { get; set; }
        public string ReferDesc { get; set; }
        public DateTime ReferDate { get; set; }
        public long? RequestID { get; set; }
        public virtual User User_ReferBy { get; set; }
        public virtual User User_ReferTo { get; set; }
        public virtual EstateFile File { get; set; }
        public virtual FileRequest Request { get; set; }
    }
}
