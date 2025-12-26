using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface ICityDataService : IBaseDataService<RealEstateAgency.Models.City, int>
    {
        bool IsDeleteValid(int id);
        IQueryable<CitySchema> Search(int provinceID);
    }
}
