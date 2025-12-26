using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IPersonDataService:IBaseDataService<Models.Person, long>
    {
        string GetNextCode();
        IQueryable<PersonSchema> Search(PersonSchema model);
        bool IsDeleteNotValid(long id);
        Person AddPerson(PersonSchema model);
        Person UpdatePerson(PersonSchema model);
    }
}
