using RealEstateAgency.Models.Helper;
using System;

namespace RealEstateAgency.Models.Schema
{
    public class FileReferSchema
    {
        public long FileReferID { get; set; }
        public long? RequestID { get; set; }
        public long FileID { get; set; }
        public int ReferedBy { get; set; }
        public string ReferByName { get; set; }
        public int ReferedTo { get; set; }
        public string ReferToName { get; set; }
        public string ReferDesc { get; set; }
        public DateTime ReferDate { get; set; }
        public string PReferDate { get { return DateHelper.ToPersian(ReferDate); } }
    }
}
