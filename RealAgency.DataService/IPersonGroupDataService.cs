using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IPersonGroupDataService:IBaseDataService<PersonGroup, int>
    {
        IQueryable<PersonGroupSchema> SearchPersonGroup(PersonGroupSchema model);
        IQueryable<PersonGroupSchema> SearchGroups(PersonGroupSchema model);
    }
}
