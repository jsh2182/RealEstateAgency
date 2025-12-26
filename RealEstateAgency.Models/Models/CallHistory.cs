namespace RealEstateAgency.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class CallHistory
    {
        public long HistoryID { get; set; }
        public long? PersonID { get; set; }

        public int? UserID { get; set; }
        public string CallNumber { get; set; }

        public DateTime CallDate { get; set; }

        public virtual User User { get; set; }
        public virtual Person Person { get; set; }
    }
}
