using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFPersonGroupDataService : EFBaseDataService<PersonGroup, int>, DataService.IPersonGroupDataService
    {
        public EFPersonGroupDataService(AgencyContext context) : base(context)
        {

        }

        public PersonGroup Get(long id)
        {
            throw new NotImplementedException();
        }

        public override Expression<Func<PersonGroup, int>> GetKey()
        {
            return p => p.GroupID;
        }
        public IQueryable<PersonGroupSchema> SearchPersonGroup(PersonGroupSchema model)
        {
            var qry = from grp in Context.PersonGroups join
                      relation in Context.PersonGroupRelations on grp.GroupID equals relation.GroupID
                      select new PersonGroupSchema
                      {
                          PersonID = relation.PersonID,
                          GroupID = grp.GroupID,
                          GroupName = grp.GroupName,
                          GroupDesc = grp.GroupDesc
                      };
            if (!string.IsNullOrEmpty(model.GroupDesc))
                qry = qry.Where(q => q.GroupDesc.Contains(model.GroupDesc));
            if (!string.IsNullOrEmpty(model.GroupName))
                qry = qry.Where(q => q.GroupName.Contains(model.GroupName));
            if (model.GroupID > 0)
                qry = qry.Where(q => q.GroupID == model.GroupID);
            if (model.PersonID >= 0)
                qry = qry.Where(q => q.PersonID == model.PersonID);
            return qry;
        }
        public IQueryable<PersonGroupSchema> SearchGroups(PersonGroupSchema model)
        {
            var qry = from grp in Context.PersonGroups
                      select new PersonGroupSchema
                      {
                          GroupID = grp.GroupID,
                          GroupName = grp.GroupName,
                          GroupDesc = grp.GroupDesc
                      };
            if (!string.IsNullOrEmpty(model.GroupDesc))
                qry = qry.Where(q => q.GroupDesc.Contains(model.GroupDesc));
            if (!string.IsNullOrEmpty(model.GroupName))
                qry = qry.Where(q => q.GroupName.Contains(model.GroupName));
            if (model.GroupID > 0)
                qry = qry.Where(q => q.GroupID == model.GroupID);
            return qry;
        }
    }
}
