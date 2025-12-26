using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstateAgency.DataService;
using RealEstateAgency.EFDataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        IPersonDataService _personDataService;
        EFPersonGroupRelationDataService _groupRelationDataService;
        EFPersonGroupDataService _personGroupDataService;
        EFPersonOtherInfoDataService _otherInfoDataService;
        public PersonController(IPersonDataService personDataService,
                                EFPersonGroupRelationDataService groupRelationDataService,
                                EFPersonGroupDataService personGroupDataService,
                                EFPersonOtherInfoDataService otherInfoDataService)
        {
            _personDataService = personDataService;
            _groupRelationDataService = groupRelationDataService;
            _personGroupDataService = personGroupDataService;
            _otherInfoDataService = otherInfoDataService;
        }
        [Control("Person","read")]
        public ActionResult Index()
        {
            return View();
        }
        [Control("Person","update")]
        [HttpPost]
        public ActionResult Submit(PersonSchema model, string groupIDs)
        {
            try
            {
                long pID = 0;
                if (model.PersonID > 0)
                    pID = _personDataService.UpdatePerson(model).PersonID;
                else
                    pID = _personDataService.AddPerson(model).PersonID;
                var lstIDs = ConvertGroupItems(groupIDs);
                AddPersonGroup(pID, lstIDs);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [Control("Person","delete")]
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            try
            {
                if(_personDataService.IsDeleteNotValid(id))
                    return Json(HttpStatusCode.Forbidden, JsonRequestBehavior.AllowGet);
                var person = _personDataService.Get(id);
                _personDataService.Delete(person);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("PersonController", "Delete", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        private List<int> ConvertGroupItems(string txt)
        {
            var list = new List<int>();
            string resultTxt = txt.Replace("[", "").Replace("]", "");
            string[] strArray = resultTxt.Split(',');
            foreach (string item in strArray)
            {
                string id = item.Replace("\\", "").Replace("\"", "");
                if (!string.IsNullOrEmpty(id))
                {
                    list.Add(Convert.ToInt32(id));
                }
            }
            return list;
        }
        [Control("PersonGroup","update")]
        private bool AddPersonGroup(long personID, List<int> lstGroupID)
        {
            try
            {
                if (lstGroupID.Count == 0)
                    return true ;
                if (personID < 1)
                    return false;
                var relations = _groupRelationDataService.All().Where(p => p.PersonID == personID).ToList();
                var lstExistsingIDs = relations.Select(r=>r.GroupID).ToList();
                if (relations != null && relations.Count > 0)
                {
                    var lstDel = relations.Where(r => !lstGroupID.Contains(r.GroupID)).ToList();
                    _groupRelationDataService.DeleteRange(lstDel);
                    relations.RemoveAll(r => !lstGroupID.Contains(r.GroupID));
                    lstGroupID.RemoveAll(g => lstExistsingIDs.Contains(g));
                }
                var lstInsert = lstGroupID.Select(g => new PersonGroupRelation
                {
                    GroupID = g,
                    PersonID = personID
                }).ToList();
                _groupRelationDataService.AddRange(lstInsert);
                return true;
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "AddPersonGroup", ex);
                return false;
            }
        }
        [HttpPost]
        public ActionResult LoadPersonGroups([DataSourceRequest]DataSourceRequest request, long personID)
        {
            try
            {
                var qry = _personGroupDataService.SearchPersonGroup(new PersonGroupSchema { PersonID = personID });
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "LoadPersonGroups", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Search([DataSourceRequest] DataSourceRequest request, PersonSchema model)
        {
            try
            {
                var qry = _personDataService.Search(model);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SubmitOtherInfo(PersonOtherInfoSchema model)
        {
            try
            {
                PersonOtherInfo info;
                if (model.PersonID < 1)
                    return Json(HttpStatusCode.NoContent, JsonRequestBehavior.AllowGet);
                if (model.InfoID > 0)
                {
                    info = _otherInfoDataService.Get(model.InfoID);
                    if (info.CreateByID != Tools.CurrentUser.UserID)
                        return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                    info.InfoDesc = model.InfoDesc;
                    _otherInfoDataService.Update(info);
                }
                else
                {
                    info = new PersonOtherInfo()
                    {
                        CreateByID = Tools.CurrentUser.UserID,
                        CreateDate = DateTime.Now,
                        InfoDesc = model.InfoDesc,
                        PersonID = model.PersonID
                    };
                    _otherInfoDataService.Add(info);
                }
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "SubmitOtherInfo", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadOtherInfoes(long personID, [DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var qry = _otherInfoDataService.GetPersonOtherInfoes(personID);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "LoadOtherInfoes", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult PersonPartial()
        {
            return PartialView();
        }
        public ActionResult GetNextCode()
        {
            try
            {
                var code = _personDataService.GetNextCode();
                return Json(code, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "GetNextCode", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);

            }
        }


    }

}