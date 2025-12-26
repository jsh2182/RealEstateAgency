using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models
{
    public class ExceptionLog
    {
        public long ID { get; set; }
        public string FormName { get; set; }
        public string MethodName { get; set; }
        public string ExcpetionDesc { get; set; }
        public DateTime ExceptionDate { get; set; }
    }
}
