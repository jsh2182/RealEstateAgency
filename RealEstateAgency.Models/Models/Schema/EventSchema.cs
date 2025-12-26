using RealEstateAgency.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgency.Models.Schema
{
    public class EventSchema
    {
        public long EventID { get; set; }
        public long? PersonID { get; set; }
        public string PersonName { get; set; }
        public string Description { get; set; }
        public long? AttachmentID { get; set; }
        public string AttachmentType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public string PCreationDate { get { return DateHelper.ToPersian(CreationDate); } }
        public int CreateByID { get; set; }
        public string CreateByName { get; set; }
        public int? Consultant { get; set; }
        public string ConsultantName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string PUpdateDate { get { return UpdateDate != null ? DateHelper.ToPersian((DateTime)UpdateDate) : ""; } }
        public int? UpdateByID { get; set; }
        public string UpdateByName { get; set; }
        public string EventType { get; set; }
        public string DetailName { get; set; }
        public string DetailValue { get; set; }
        public ICollection<EventDetail> Details { get; set; }
    }
}
