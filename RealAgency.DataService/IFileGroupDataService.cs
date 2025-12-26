using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IFileGroupDataService : IBaseDataService<Models.FileGroup, long>
    {
        IQueryable<FileGroupSchema> Search(FileGroupSchema model);
    }
}
