using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Schema
{
    public class FileRequestSchema
    {
        public long FileRequestID { get; set; }
        public long FileID { get; set; }
        public int RequestBy { get; set; }
        public string RequestDesc { get; set; }
        public DateTime RequestDate { get; set; }
        public string PRequestDate { get { return Helper.DateHelper.ToPersian(RequestDate); } }
        public string RequestByName { get; set; }
        public bool? IsRefered { get; set; }
        public string FileCode { get; set; }
        public string RequestType { get; set; }
    }
}
