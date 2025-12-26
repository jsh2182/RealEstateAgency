namespace RealEstateAgency.Models
{
    using System.Collections.Generic;

    public class FileGroup
    {
        public FileGroup()
        {
            FileGroupRelations = new List<FileGroupRelation>();
        }

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public virtual ICollection<FileGroupRelation> FileGroupRelations { get; set; }
    }
}
