using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IFileRequestDataService: IBaseDataService<FileRequest, long>
    {
        IQueryable<FileRequestSchema> Search(FileRequestSchema model);
        FileRequest UpdateRequest(FileRequestSchema model);
        FileRequest AddRequest(FileRequestSchema model);
        Enums.RemoveResult RemoveRequest(long id);
        bool RequestExists(int userID, long fileID);
    }
}
