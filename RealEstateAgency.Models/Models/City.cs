namespace RealEstateAgency.Models
{
    using System.Collections.Generic;
    public class City
    {
        public City()
        {
            EstateFiles = new List<EstateFile>();
            Zones = new List<Zone>();
            People = new List<Person>();
        }

        public int CityID { get; set; }

        public int ProvinceID { get; set; }
        public string CityName { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<EstateFile> EstateFiles { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
