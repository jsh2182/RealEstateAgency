using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Schema
{
    public class CitySchema
    {
        public int CityID { get; set; }

        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
    }
}
