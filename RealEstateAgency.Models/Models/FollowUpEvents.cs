using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models
{
    public class FollowUpEvents
    {
        public long FollowUpID { get; set; }
        public long PersonEventID { get; set; }
        public int? FollowUpUserID { get; set; }
        public DateTime FollowDate { get; set; }
        public string FollowUpDesc { get; set; }
        public bool IsDone { get; set; }
        public decimal FollowUpCode { get; set; }
        public string FollowUpResult { get; set; }
        public virtual PersonEvents PersonEvent { get; set; }
        public virtual User FollowUpUser { get; set; }

    }
}
