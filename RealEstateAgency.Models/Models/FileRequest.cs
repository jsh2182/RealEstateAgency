using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models
{
    public class FileRequest
    {
        public FileRequest()
        {
            Refers = new List<FileRefer>();
        }
        public long FileRequestID { get; set; }
        public long FileID { get; set; }
        public int RequestBy { get; set; }
        public string RequestDesc { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsRefered { get; set; }
        public virtual User  RequestByUser { get; set; }
        public virtual EstateFile File { get; set; }
        public virtual ICollection<FileRefer> Refers { get; set; }
    }
}
