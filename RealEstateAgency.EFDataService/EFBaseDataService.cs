using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgency.Models;

namespace RealEstateAgency.EFDataService
{
    public abstract class EFBaseDataService<TModel,TKey> where TModel : class 
    {
        public AgencyContext Context { get; set; }
        public DbSet<TModel> Current { get; set; }

        public abstract System.Linq.Expressions.Expression<Func<TModel, TKey>> GetKey();

        public EFBaseDataService(AgencyContext Context)
        {
            this.Context = Context;

            ((IObjectContextAdapter)Context).ObjectContext.CommandTimeout = 0;

            this.Current = Context.Set<TModel>();

        }

        public virtual TModel Add(TModel item)
        {
            Current.Add(item);

            Context.SaveChanges();

            return item;
        }
        public virtual List<TModel> AddRange(List<TModel> items)
        {
            Current.AddRange(items);

            Context.SaveChanges();

            return items;
        }

        public virtual TModel Update(TModel item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();

            return item;
        }

        public virtual TModel Delete(TModel item)
        {
            Current.Remove(item);
            Context.SaveChanges();
            return item;
        }
        public virtual TModel Get(TKey id)
        {
            var key = GetKey();
            var body = key.Body;
            var exp = System.Linq.Expressions.Expression.Equal(body, System.Linq.Expressions.Expression.Constant(id));
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<TModel, bool>>(exp, key.Parameters);

            return Current.FirstOrDefault(lambda);
        }

        public virtual IQueryable<TModel> All()
        {
            return Current;
        }

        public virtual IQueryable<TModel> All(int PageSize, int PageIndex)
        {
            return Current.OrderBy(GetKey()).Skip(PageSize * PageIndex).Take(PageSize);
        }

        public long Count()
        {
            return Current.LongCount();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public virtual List<TModel> DeleteRange(List<TModel> items)
        {
                foreach (var item in items)
                    if (Context.Entry(item).State == EntityState.Detached)
                        Current.Attach(item);

                Current.RemoveRange(items);
                Context.SaveChanges();
                return items;

        }
    }
}
