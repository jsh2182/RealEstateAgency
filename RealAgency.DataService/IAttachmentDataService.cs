using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IAttachmentDataService:IBaseDataService<Models.Attachment,long>
    {
        IQueryable<AttachementSchema> Search(AttachementSchema model);
    }
}
