using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class PersonEventsController : Controller
    {
        // GET: Event
        IEventDetailDataService _eventDetailDataService;
        IPersonEventDataService _eventDataService;
        IAttachmentDataService _attachmentDataService;
        IFollowUpEventsDataService _followUpDataService;

        public PersonEventsController(IEventDetailDataService eventDetailDataService,
                                      IPersonEventDataService eventDataService,
                                      IAttachmentDataService attachmentDataService,
                                      IFollowUpEventsDataService followUpDataService)
        {
            _eventDataService = eventDataService;
            _eventDetailDataService = eventDetailDataService;
            _attachmentDataService = attachmentDataService;
            _followUpDataService = followUpDataService;
        }
        [Control("PersonEvents","read")]
        public ActionResult Index()
        {
            return View();
        }
        #region FollowUp
        [Control("FollowUpEvents", "read")]
        public ActionResult FollowUpEvents()
        {
            return View();
        }
        //[HttpGet]
        public ActionResult FollowUpPartial()
        {
            return PartialView();
        }
        [Control("PersonEvents", "update")]
        [HttpPost]
        public ActionResult SubmitFollowUp(FollowUpEventsSchema model)
        {
            try
            {
                if (_followUpDataService.FollowUpExists(model.PersonEventID, model.FollowDate))
                    return Json(HttpStatusCode.Conflict, JsonRequestBehavior.AllowGet);
                if (model.FollowUpID == 0)
                    _followUpDataService.AddFollowUp(model);
                else
                    _followUpDataService.EditFollowUp(model);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonEventController", "SubmitFollowUp", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SearchFollowUps([DataSourceRequest]DataSourceRequest request, FollowUpEventsSchema model)
        {
            try
            {
                var qry = _followUpDataService.Search(model);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonEventsController", "SearchFollowUps", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }


        }
        [Control("PersonEvents", "update")]
        [HttpPost]
        public ActionResult DoneEvent(long followUpID, string followUpResult, decimal followUpCode)
        {
            try
            {
                _followUpDataService.DoneFollowUp(followUpID, followUpResult, followUpCode);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonEventController", "DoneEvent", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoadFollowUpStatus()
        {
            var s = new { ID = "null", Name = "کلیه موارد" };
            var s1 = new { ID = "false", Name = "پیگیری نشده" };
            var s2 = new { ID = "true", Name = "پیگیری شده" };
            var lst = (new[] { s, s1, s2 }).ToList();
            return Json(new SelectList(lst, "ID", "Name"), JsonRequestBehavior.AllowGet);
        }

        #endregion
        public ActionResult PersonEventsPartial(long eventID)
        {
            ViewBag.EventID = eventID;
            return PartialView();
        }
        [HttpPost]
        public ActionResult GetEvent(long eventID)
        {
            try
            {
                var e = _eventDataService.Get(eventID);
                var schema = new EventSchema()
                {
                        ConsultantName = e.ConsultantUser.Name,
                        CreateByName = e.CreateUser.Name,
                        CreationDate = e.CreationDate,
                        Description = e.Description,
                        EventType = e.EventType,
                        PersonName = e.Person.PersonName                        
                };
                return Json(schema, JsonRequestBehavior.AllowGet);
                 
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonEventsController", "GetEvent", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SearchEvents([DataSourceRequest]DataSourceRequest request, EventSchema model)
        {
            try
            {
                var qry = _eventDataService.Search(model).ToList();
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EventController", "SearchEvents", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SearchDetails(DataSourceRequest request, long eventID)
        {
            try
            {
                var qry = _eventDetailDataService.Search(eventID);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EventController", "SearchDetails", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult LoadEventType()
        {
            var lst = new List<string>() { "کارشناسی", "سرویس حضوری", "مشتری تلفنی", "ارسال آگهی", "پیگیری مشتری", "پیگیری مالک", "انتقال مشتری" };
            return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }).ToList(), "ID", "Name"), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult LoadDetailNames(string eventType)
        {
            var lst = new List<string>();
            switch (eventType)
            {
                case "کارشناسی":
                    lst.AddRange(new List<string> { "محل کارشناسی", "فایل دریافتی" });
                    break;
                case "سرویس حضوری":
                    lst.AddRange(new List<string> { "بودجه", "خواسته مشتری", "مکان سرویس", "محل دریافت مشتری" });
                    break;
                case "مشتری تلفنی":
                case "ارسال آگهی":
                    lst.AddRange(new List<string> { "پنل", "دیوار", "آی هوم", "سایت", "آواسنتر", "شابش" });
                    break;
                case "پیگیری مشتری":
                    lst.AddRange(new List<string> { "بودجه", "خواسته مشتری" });
                    break;
                case "پیگیری مالک":
                    lst.AddRange(new List<string> { "منطقه", "متراژ", "قیمت اعلامی", "توافق کمیسیون" });
                    break;
                case "انتقال مشتری":
                    lst.AddRange(new List<string> { "تلفن", "نوع درخواست", "ارجاع به مشاور" });
                    break;
            }
            return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }).ToList(), "ID", "Name"), JsonRequestBehavior.AllowGet); ;

        }
        [Control("PersonEvents", "update")]
        [HttpPost]
        public ActionResult SubmitEvent(EventSchema model)
        {
            try
            {
                if (model.EventID > 0)
                    _eventDataService.UpdateEvent(model);
                else
                    _eventDataService.AddEvent(model);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EventController", "SubmitEvent", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [Control("PersonEvents", "delete")]
        [HttpDelete]
        public ActionResult DeleteEvent(long eventID)
        {
            try
            {
                var attachs = _attachmentDataService.All().Where(a => a.AttachmentType == "Events" && a.ParentID == eventID).ToList();
                attachs.ForEach(a => a.IsDeleted = true);
                foreach (var a in attachs)
                {
                    _attachmentDataService.Update(a);
                }
                var e = _eventDataService.Get(eventID);
                _eventDataService.Delete(e);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("EventConyroller", "DeleteEvent", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [Control("PersonEvents", "update")]
        [HttpPost]
        public ActionResult SubmitEventDetail(EventDetailSchema model)
        {
            try
            {
                EventDetail detail;
                if (model.DetailID > 0)
                {
                    detail = _eventDetailDataService.Get(model.DetailID);
                }
                else
                {
                    detail = new EventDetail();
                    detail.EventID = model.EventID;
                }
                detail.DetailName = model.DetailName;
                detail.DetailValue = model.DetailValue;
                if (model.DetailID == 0)
                    _eventDetailDataService.Add(detail);
                else
                    _eventDetailDataService.Update(detail);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("EventController", "SubmitEventDetails", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }

        }
        [Control("PersonEvents", "delete")]
        [HttpDelete]
        public ActionResult DeleteEventDetail(long detailID)
        {
            try
            {
                var detail = _eventDetailDataService.Get(detailID);
                _eventDetailDataService.Delete(detail);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("EventController", "DeleteEventDetail", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetNextFollowUpCode(long eventID)
        {
            try
            {
                var code = _followUpDataService.GetNextCode(eventID);
                return Json(code, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonEventController", "GetNextFollowUpCode", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}