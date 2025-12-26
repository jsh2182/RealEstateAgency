using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateAgency.EFDataService
{
    public class EFFollowUpEventsDataService : EFBaseDataService<FollowUpEvents, long>, IFollowUpEventsDataService
    {
        public EFFollowUpEventsDataService(AgencyContext context) : base(context)
        {

        }
        public override Expression<Func<FollowUpEvents, long>> GetKey()
        {
            return e => e.FollowUpID;
        }
        public IQueryable<FollowUpEventsSchema> Search(FollowUpEventsSchema model)
        {
            var qry = All();
            if (model.FollowDate > DateTime.MinValue)
            {
                qry = qry.Where(q => q.FollowDate == model.FollowDate);
            }
            else
            {
                var now = DateTime.Now.Date;
                qry = qry.Where(q => q.FollowDate >= now);
            }
            if (!string.IsNullOrEmpty(model.FollowUpDesc))
                qry = qry.Where(q => q.FollowUpDesc.Contains(model.FollowUpDesc));
            if (model.FollowUpID > 0)
                qry = qry.Where(q => q.FollowUpID == model.FollowUpID);
            if (Tools.CurrentUser.IsAdmin)
            {
                if (model.FollowUpUserID.HasValue)
                    qry = qry.Where(q => q.FollowUpUserID == model.FollowUpUserID);
            }
            else
                qry = qry.Where(q => q.FollowUpUserID == Tools.CurrentUser.UserID);
            if (Tools.CurrentUser.IsAdmin)
            {
                if (model.IsDone.HasValue)
                    qry = qry.Where(q => q.IsDone == model.IsDone);
            }
            else
                qry = qry.Where(q => q.IsDone);
            if (model.PersonEventID > 0)
                qry = qry.Where(q => q.PersonEventID == model.PersonEventID);
            var result = qry.Select(q => new FollowUpEventsSchema
            {
                FollowDate = q.FollowDate,
                FollowUpDesc = q.FollowUpDesc,
                FollowUpID = q.FollowUpID,
                FollowUpUserID = q.FollowUpUserID,
                FollowUpUserName = q.FollowUpUser.Name,
                IsDone = q.IsDone,
                PersonEventID = q.PersonEventID,
                FollowUpResult = q.FollowUpResult,
                FollowUpCode = q.FollowUpCode
            });
            return result;
        }
        private FollowUpEvents Init(FollowUpEventsSchema model)
        {
            FollowUpEvents followUpEvent;
            if (model.FollowUpID > 0)
            {
                followUpEvent = Get(model.FollowUpID);
            }
            else
            {
                followUpEvent = new FollowUpEvents
                {
                    PersonEventID = model.PersonEventID,
                    FollowUpCode = GetNextCode(model.PersonEventID)// model.FollowUpCode
                };
            }

            followUpEvent.FollowDate = model.FollowDate;
            followUpEvent.FollowUpDesc = model.FollowUpDesc;
            followUpEvent.FollowUpUserID = model.FollowUpUserID;
            if (model.IsDone.HasValue)
            {
                followUpEvent.IsDone = (bool)model.IsDone;
                followUpEvent.FollowUpResult = model.FollowUpResult;
            }
            return followUpEvent;
        }
        public FollowUpEvents AddFollowUp(FollowUpEventsSchema model)
        {
            var followUp = Init(model);
            followUp = Add(followUp);
            return followUp;
        }
        public FollowUpEvents EditFollowUp(FollowUpEventsSchema model)
        {
            var followUp = Init(model);
            followUp = Update(followUp);
            return followUp;
        }
        public bool FollowUpExists(long eventID, DateTime followDate)
        {
            return All().Any(f => f.PersonEventID == eventID && f.FollowDate == followDate);
        }
        public void DoneFollowUp(long followUpID, string followUpResult, decimal followUpCode)
        {
            var e = Get(followUpID);
            e.IsDone = true;
            e.FollowUpResult = followUpResult;
            e.FollowUpCode = followUpCode;
            Update(e);
        }
        public decimal GetNextCode(long eventID)
        {
            decimal code = 1;
            if (All().Any(f => f.PersonEventID == eventID))
                code = All().Where(f => f.PersonEventID == eventID).Max(f => f.FollowUpCode) + 1;
            return code;
        }

    }
}
