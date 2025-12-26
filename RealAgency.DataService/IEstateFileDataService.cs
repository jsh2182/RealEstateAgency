using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IEstateFileDataService:IBaseDataService<EstateFile, long>
    {
        IQueryable<EstateFileSchema> Search(EstateFileSchema model);
        EstateFile AddEstateFile(EstateFileSchema model);
        EstateFile UpdateEstateFile(EstateFileSchema model);
        EstateFileSchema GetEstateFile(long id);
        string GetNextFileCode();

    }
}
