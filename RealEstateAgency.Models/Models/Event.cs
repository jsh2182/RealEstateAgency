namespace RealEstateAgency.Models
{
    using System;

    public class Event
    {
        public long EventID { get; set; }
        public long PersonID { get; set; }
        public string Description { get; set; }
        public long? AttachmentID { get; set; }
        public string AttachmentType { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreateByID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByID { get; set; }
        public virtual User CreateUser { get; set; }
        public virtual User UpdateUser { get; set; }
        public virtual Person Person { get; set; }
    }
}
