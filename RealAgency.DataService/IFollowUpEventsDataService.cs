using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq;

namespace RealEstateAgency.DataService
{
    public interface IFollowUpEventsDataService:IBaseDataService<FollowUpEvents,long>
    {
        IQueryable<FollowUpEventsSchema> Search(FollowUpEventsSchema model);
        FollowUpEvents AddFollowUp(FollowUpEventsSchema model);
        FollowUpEvents EditFollowUp(FollowUpEventsSchema model);
        bool FollowUpExists(long eventID, DateTime followDate);
        void DoneFollowUp(long followUpID, string followUpResult, decimal followUpCode);
        decimal GetNextCode(long eventID);
    }
}
