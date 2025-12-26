using RealEstateAgency.Models.Helper;
using System;

namespace RealEstateAgency.Models.Schema
{
    public class CallReferSchema
    {
        public int ReferID { get; set; }
        public int ReferTo { get; set; }
        public string ReferToName { get; set; }
        public int ReferBy { get; set; }
        public string ReferByName { get; set; }
        public DateTime ReferDate { get; set; }
        public string PReferDate { get { return DateHelper.ToPersian(ReferDate); } }
        public string ReferDesc { get; set; }
    }
}
