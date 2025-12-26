namespace RealEstateAgency.Models
{
    public class PersonGroupRelation
    {
        public long RelationID { get; set; }
        public long PersonID { get; set; }
        public int GroupID { get; set; }
        public virtual Person Person { get; set; }
        public virtual PersonGroup PersonGroup { get; set; }
    }
}
