using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Schema
{
    public class PersonOtherInfoSchema
    {
        public long InfoID { get; set; }
        public int CreateByID { get; set; }
        public string CreateByName { get; set; }
        public DateTime CreateDate { get; set; }
        public long PersonID { get; set; }
        public string PersonName { get; set; }
        public string InfoDesc { get; set; }
    }
}
