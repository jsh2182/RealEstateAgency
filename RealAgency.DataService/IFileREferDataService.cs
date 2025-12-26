using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IFileReferDataService:IBaseDataService<Models.FileRefer,long>
    {
        IQueryable<FileReferSchema> Search(FileReferSchema model);
        FileRefer AddRefer(FileReferSchema model);
        FileRefer UpdateRefer(FileReferSchema model);
        void DeleteRefer(long referID);
    }
}
