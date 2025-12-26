using RealEstateAgency.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.EFDataService
{
    public class EFPersonOtherInfoDataService : EFBaseDataService<PersonOtherInfo, long>, DataService.IPersonOtherInfoDataService
    {
        public EFPersonOtherInfoDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<PersonOtherInfo, long>> GetKey()
        {
            return p => p.InfoID;
        }
        public IQueryable<PersonOtherInfoSchema> GetPersonOtherInfoes(long personID)
        {
            var qry = All().Where(p => p.PersonID == personID)
                            .Select(p => new PersonOtherInfoSchema
                            {
                                CreateByID = p.CreateByID,
                                CreateByName = p.User.Name,
                                CreateDate = p.CreateDate,
                                InfoDesc = p.InfoDesc,
                                InfoID = p.InfoID,
                                PersonID = p.PersonID
                            });
            return qry;

        }
    }
}
