using RealEstateAgency.Models.Helper;
using System;

namespace RealEstateAgency.Models.Schema
{
    public class AttachementSchema
    {
        public long AttachmentID { get; set; }
        public string AttachmentName { get; set; }
        public int AttachedBy { get; set; }
        public string AttachByName { get; set; }
        public DateTime AttachDate { get; set; }
        public string  PAttachDate { get { return DateHelper.ToPersian(AttachDate); } }
        public byte[] AttachContent { get; set; }
        public string AttachmentType { get; set; }
        public long? ParentID { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public string DeleteByName { get; set; }
    }
}
