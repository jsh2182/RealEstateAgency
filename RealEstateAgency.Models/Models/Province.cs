namespace RealEstateAgency.Models
{
    using System.Collections.Generic;

    public partial class Province
    {

        public Province()
        {
            Cities = new List<City>();
            EstateFiles = new List<EstateFile>();
            People = new List<Person>();
        }

        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<EstateFile> EstateFiles { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
