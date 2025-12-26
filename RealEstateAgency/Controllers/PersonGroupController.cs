using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.Controllers
{
    public class PersonGroupController : System.Web.Mvc.Controller
    {
        private readonly IPersonGroupDataService _personGroupDataService;
        private readonly IPersonGroupRelationDataService _personGroupRelationDataService;
        public PersonGroupController(IPersonGroupDataService personGroupDataService, IPersonGroupRelationDataService personGroupRelationDataService)
        {
            _personGroupDataService = personGroupDataService;
            _personGroupRelationDataService = personGroupRelationDataService;
        }
        // GET: PersonGroup
        //[Auth("PersonGroup")]
       [Control("PersonGroup","read")]
        public ActionResult Index()
        {
            return View();
        }
        private PersonGroup InsertGroup(PersonGroupSchema model)
        {
            try
            {
                var grp = new PersonGroup()
                {
                    GroupDesc = model.GroupDesc,
                    GroupName = model.GroupName
                };
                _personGroupDataService.Add(grp);
                return grp;
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonGroupController", "InsertGroup", ex);
                return null;
            }
        }
        
        private PersonGroup UpdateGroup(PersonGroupSchema model)
        {
            try
            {
                var grp = _personGroupDataService.Get(model.GroupID);
                if (grp == null || grp.GroupID == 0)
                    return null;
                grp.GroupName = model.GroupName;
                grp.GroupDesc = model.GroupDesc;
               grp = _personGroupDataService.Update(grp);
                return grp;
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonGroupController", "UpdateGroup", ex);
                return null;

            }
        }
        [HttpDelete]
        public ActionResult RemoveGroup(int groupID)
        {
            try
            {
                if (!IsDeleteValid(groupID))
                    return Json(new { Message = "اشخاصی برای این گروه تعریف شده است و امکان حذف آن وجود ندارد" }, JsonRequestBehavior.AllowGet);
                var grp = _personGroupDataService.Get(groupID);
                _personGroupDataService.Delete(grp);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("PersonGroupController", "RemovePerson", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        private bool IsDeleteValid(int id)
        {
            try
            {
               var result = _personGroupRelationDataService.All().Any(p => p.PersonGroup.GroupID == id);
                return !result;
            }
            catch (Exception ex)
            {
                Tools.Add2Log("PersonGroupController", "IsDeleteValid", ex);
                return false;
            }
        }
        [HttpPost]
        public ActionResult SubmitPersonGroup(PersonGroupSchema model)
        {
            try
            {
                if (model.GroupID < 1)
                    InsertGroup(model);
                else
                    UpdateGroup(model);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonGroupController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Search(PersonGroupSchema model, [DataSourceRequest] DataSourceRequest command)
        {
            try
            {
                var qry = _personGroupDataService.SearchGroups(model);
                return Json(qry.ToDataSourceResult(command), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("PErsonGroupController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SearchPersonGroups(PersonGroupSchema model, [DataSourceRequest] DataSourceRequest command)
        {
            try
            {
                var qry = _personGroupDataService.SearchPersonGroup(model);
                return Json(qry.ToDataSourceResult(command), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("PErsonGroupController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadAllGroups([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var qry =_personGroupDataService.All().Select(g => new PersonGroupSchema { 
                    GroupDesc = g.GroupDesc,
                    GroupID = g.GroupID,
                    GroupName = g.GroupName
                });
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonGroupController", "LoadAllGroups", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }

    }
}