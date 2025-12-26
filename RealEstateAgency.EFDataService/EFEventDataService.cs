using RealEstateAgency.Models;
using System;
using System.Linq.Expressions;
using RealEstateAgency.DataService;
using RealEstateAgency.Models.Schema;
using System.Linq;

namespace RealEstateAgency.EFDataService
{
    public class EFEventDataService : EFBaseDataService<PersonEvents, long>, IPersonEventDataService
    {
        public EFEventDataService(AgencyContext context):base(context)
        {

        }
        public override Expression<Func<PersonEvents, long>> GetKey()
        {
            return e => e.EventID;
        }
        public IQueryable<EventSchema> Search(EventSchema model)
        {
            var qry = All();
            if (model.CreateByID > 0)
                qry = qry.Where(q => q.CreateByID == model.CreateByID);
            if(model.CreationDate >  DateTime.MinValue)
            {
                qry = qry.Where(q => q.CreationDate >= model.CreationDate);
            }
            if (model.CreationDateTo.HasValue)
                qry = qry.Where(q => q.CreationDate <= model.CreationDateTo);
            if (!string.IsNullOrEmpty(model.Description))
                qry = qry.Where(q => q.Description.Contains(model.Description));
            if (!string.IsNullOrEmpty(model.DetailName))
                qry = qry.Where(q => q.Details.Any(d => d.DetailName == model.DetailName));
            if (!string.IsNullOrEmpty(model.DetailValue))
                qry = qry.Where(q => q.Details.Any(d => d.DetailValue == model.DetailValue));
            if (!string.IsNullOrEmpty(model.EventType))
                qry = qry.Where(q => q.EventType == model.EventType);
            if (model.EventID > 0)
                qry = qry.Where(q => q.EventID == model.EventID);
            if (model.Consultant > 0)
                qry = qry.Where(q => q.Consultant == model.Consultant);
            return qry.Select(q => new EventSchema
            {
                EventID = q.EventID,
                CreateByID = q.CreateByID,
                PersonID = q.PersonID,                
                Consultant = q.Consultant,
                ConsultantName = q.ConsultantUser.Name,
                EventType = q.EventType,
                PersonName = q.Person.PersonName,
                CreationDate = q.CreationDate,
                Description = q.Description,
                Details = q.Details
            });
        }
        private PersonEvents InitEvent(EventSchema model)
        {
            PersonEvents e;

            if (model.EventID > 0)
            {
                e = Get(model.EventID);
                e.UpdateByID = Tools.CurrentUser.UserID;
                e.UpdateDate = DateTime.Now;
            }
            else
            {
                e = new PersonEvents
                {
                    CreateByID = Tools.CurrentUser.UserID,
                    CreationDate = DateTime.Now,
                    //Details = model.Details.Select(d => new EventDetail
                    //{
                    //    DetailName = d.DetailName,
                    //    DetailValue = d.DetailValue
                    //}).ToList()
                };

            }
            e.Description = model.Description;
            e.EventType = model.EventType;
            e.PersonID = model.PersonID;
            e.Consultant = model.Consultant;
            return e;
        }
        public PersonEvents UpdateEvent(EventSchema model)
        {
            var e = InitEvent(model);
            return Update(e);
        }
        public PersonEvents AddEvent(EventSchema model)
        {
            var e = InitEvent(model);
            return Add(e);
        }
    }
}
