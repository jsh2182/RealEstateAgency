using RealEstateAgency.Models.Helper;
using System;

namespace RealEstateAgency.Models.Schema
{
    public class CallHistorySchema
    {
        public long HistoryID { get; set; }
        public long? PersonID { get; set; }
        public string PersonName { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string CallNumber { get; set; }
        public DateTime CallDate { get; set; }
        public string PCallDate { get { return DateHelper.ToPersian(CallDate); } }
    }
}
