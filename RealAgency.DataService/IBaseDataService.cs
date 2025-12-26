using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgency.DataService
{
   public interface IBaseDataService<TModel, TKey>
    {
        TModel Add(TModel item);
        List<TModel> AddRange(List<TModel> items);
        TModel Update(TModel item);
        TModel Delete(TModel item);
        List<TModel> DeleteRange(List<TModel> iems);
        TModel Get(TKey id);
        IQueryable<TModel> All();
        IQueryable<TModel> All(int PageSize, int PageIndex);
        long Count();
        int Save();
    }
}
