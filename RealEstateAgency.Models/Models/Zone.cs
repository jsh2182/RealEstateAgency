namespace RealEstateAgency.Models
{
    using System.Collections.Generic;

    public class Zone
    {
        public Zone()
        {
            EstateFiles = new List<EstateFile>();
            People = new List<Person>();
        }

        public int ZoneID { get; set; }

        public int CityID { get; set; }
        public string ZoneName { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<EstateFile> EstateFiles { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
