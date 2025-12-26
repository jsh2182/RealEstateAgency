namespace RealEstateAgency.Models
{
    public partial class FileGroupRelation
    {
        public long RelationID { get; set; }
        public long FileID { get; set; }
        public int GroupID { get; set; }
        public virtual EstateFile EstateFile { get; set; }
        public virtual FileGroup FileGroup { get; set; }
    }
}
