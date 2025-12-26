using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Schema
{
    public class EventDetailSchema
    {
        public long DetailID { get; set; }
        public string DetailName { get; set; }
        public string DetailValue { get; set; }
        public long EventID { get; set; }
    }
}
