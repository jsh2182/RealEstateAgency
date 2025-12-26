using System;

namespace RealEstateAgency.Models.Schema
{
    public class FollowUpEventsSchema
    {
        public long FollowUpID { get; set; }
        public long PersonEventID { get; set; }
        public int? FollowUpUserID { get; set; }
        public string FollowUpUserName { get; set; }
        public string PersonName { get; set; }
        public DateTime FollowDate { get; set; }
        public string PFollowDate { get { return Helper.DateHelper.ToPersian(FollowDate); } }
        public string FollowUpDesc { get; set; }
        public bool? IsDone { get; set; }
        public string VisibileProp { get { return IsDone == true ? "hidden" : "visible"; } }
        public decimal FollowUpCode { get; set; }
        public string FollowUpResult { get; set; }
    }
}
