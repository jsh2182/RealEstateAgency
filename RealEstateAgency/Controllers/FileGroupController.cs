using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using RealEstateAgency.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace RealEstateAgency.Controllers
{
    public class FileGroupController : System.Web.Mvc.Controller
    {
        // GET: FileGroup
        IFileGroupDataService _fileGroupDataService;
        IFileGroupRelationDataService _fileGroupRelationDataService;
        public FileGroupController(IFileGroupDataService fileGroupDataService, IFileGroupRelationDataService fileGroupRelationDataService)
        {
            _fileGroupDataService = fileGroupDataService;
            _fileGroupRelationDataService = fileGroupRelationDataService;

        }
        public ActionResult Index()
        {
            return View();
        }
        private FileGroup InsertGroup(FileGroupSchema model)
        {
            try
            {
                var grp = new FileGroup()
                {
                    GroupDesc = model.GroupDesc,
                    GroupName = model.GroupName
                };
                _fileGroupDataService.Add(grp);
                return grp;
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileGroupController", "InsertGroup", ex);
                return null;
            }
        }
        private FileGroup UpdateGroup(FileGroupSchema model)
        {
            try
            {
                var grp = _fileGroupDataService.Get(model.GroupID);
                if (grp == null || grp.GroupID == 0)
                    return null;
                grp.GroupName = model.GroupName;
                grp.GroupDesc = model.GroupDesc;
                grp = _fileGroupDataService.Update(grp);
                return grp;
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileGroupController", "UpdateGroup", ex);
                return null;

            }
        }
        [HttpDelete]
        public ActionResult RemoveGroup(int groupID)
        {
            try
            {
                if (!IsDeleteValid(groupID))
                    return Json(new { Message = "فایل هایی برای این گروه تعریف شده است و امکان حذف آن وجود ندارد" }, JsonRequestBehavior.AllowGet);
                var grp = _fileGroupDataService.Get(groupID);
                _fileGroupDataService.Delete(grp);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("FileGroupController", "RemovePerson", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        private bool IsDeleteValid(int id)
        {
            try
            {
                var result = _fileGroupRelationDataService.All().Any(p => p.FileGroup.GroupID == id);
                return !result;
            }
            catch (Exception ex)
            {
                Tools.Add2Log("FileGroupController", "IsDeleteValid", ex);
                return false;
            }
        }
        [HttpPost]
        public ActionResult Submit(FileGroupSchema model)
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

                Tools.Add2Log("FileGroupController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Search(FileGroupSchema model, [DataSourceRequest] DataSourceRequest command)
        {
            try
            {
                var qry = _fileGroupDataService.Search(model);
                return Json(qry.ToDataSourceResult(command), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileGroupController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadAllGroups([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var qry = _fileGroupDataService.All().Select(g => new FileGroupSchema
                {
                    GroupDesc = g.GroupDesc,
                    GroupID = g.GroupID,
                    GroupName = g.GroupName
                });
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileGroupController", "LoadAllGroups", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}