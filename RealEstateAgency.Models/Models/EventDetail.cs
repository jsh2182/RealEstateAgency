using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models
{
    public class EventDetail
    {
        public long DetailID { get; set; }
        public string DetailName { get; set; }
        public string DetailValue { get; set; }
        public long EventID { get; set; }
        public PersonEvents Event { get; set; }
    }
}
