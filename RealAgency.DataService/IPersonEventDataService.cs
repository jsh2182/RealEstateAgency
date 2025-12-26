using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IPersonEventDataService:IBaseDataService<PersonEvents, long>
    {
        IQueryable<EventSchema> Search(EventSchema model);
        PersonEvents UpdateEvent(EventSchema model);
        PersonEvents AddEvent(EventSchema model);
    }
}
