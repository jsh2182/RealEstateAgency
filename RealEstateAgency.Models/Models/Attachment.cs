namespace RealEstateAgency.Models
{
    using System;

    public partial class Attachment
    {
        public long AttachmentID { get; set; }

        public string AttachmentName { get; set; }

        public int AttachedBy { get; set; }

        public DateTime AttachDate { get; set; }

        public byte[] AttachContent { get; set; }

        public string AttachmentType { get; set; }

        public long? ParentID { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public virtual User AttachByUser { get; set; }

        public virtual User DeleteByUser { get; set; }
    }
}
