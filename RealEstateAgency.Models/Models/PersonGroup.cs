namespace RealEstateAgency.Models
{
    using System.Collections.Generic;

    public class PersonGroup
    {
        public PersonGroup()
        {
            PersonGroupRelations = new List<PersonGroupRelation>();
        }

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public virtual ICollection<PersonGroupRelation> PersonGroupRelations { get; set; }
    }
}
