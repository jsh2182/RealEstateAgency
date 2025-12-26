using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Schema
{
    public class FileGroupSchema
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public long FileID { get; set; }
    }
}
