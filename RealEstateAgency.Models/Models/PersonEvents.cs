namespace RealEstateAgency.Models
{
    using System;
    using System.Collections.Generic;

    public class PersonEvents
    {
        public PersonEvents()
        {
            Details = new List<EventDetail>();
            FollowUpEvents = new List<FollowUpEvents>();
        }
        public long EventID { get; set; }
        public long? PersonID { get; set; }
        public string Description { get; set; }
        public long? AttachmentID { get; set; }
        public string AttachmentType { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Consultant { get; set; }
        public int CreateByID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByID { get; set; }
        public string EventType { get; set; }
        public virtual User CreateUser { get; set; }
        public virtual User UpdateUser { get; set; }
        public virtual User ConsultantUser { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<EventDetail> Details { get; set; }
        public virtual ICollection<FollowUpEvents> FollowUpEvents { get; set; }
    }
}
