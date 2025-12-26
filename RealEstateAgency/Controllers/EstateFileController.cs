using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using RealEstateAgency.DataService;
using Kendo.Mvc.Extensions;

namespace RealEstateAgency.Controllers
{
    public class EstateFileController : System.Web.Mvc.Controller
    {
        // GET: EstateFile
        IEstateFileDataService _fileDataService;
        IFileGroupRelationDataService _fileRelationDataService;
        IFileGroupDataService _fileGroupDataService;
        [Control("EstateFile","read")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EstateFilePartial()
        {
            return PartialView();
        }

        public EstateFileController(IEstateFileDataService fileDataService, IFileGroupRelationDataService fileRelationDataService,
                                    IFileGroupDataService fileGroupDataService)
        {
            _fileDataService = fileDataService;
            _fileRelationDataService = fileRelationDataService;
            _fileGroupDataService = fileGroupDataService;
        }
        [Control("EstateFile","update")]
        [HttpPost]
        public ActionResult Submit(EstateFileSchema model, string groupIDs)
        {
            try
            {
                long fID = 0;
                if (model.FileID > 0)
                    fID = _fileDataService.UpdateEstateFile(model).FileID;
                else
                    fID = _fileDataService.AddEstateFile(model).FileID;
                var lstIDs = ConvertGroupItems(groupIDs);
                AddFileGroup(fID, lstIDs);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [Control("EstateFile", "delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var file = _fileDataService.Get(id);
                _fileDataService.Delete(file);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("EstateFileController", "LoadRestRooms", ex);
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
        [Control("FileGroup", "update")]
        private bool AddFileGroup(long fileID, List<int> lstGroupID)
        {
            try
            {
                if (lstGroupID.Count == 0)
                    return true;
                if (fileID < 1)
                    return false;
                var relations = _fileRelationDataService.All().Where(e => e.FileID == fileID).ToList();
                var existingIDs = relations.Select(r => r.GroupID);
                if (relations != null && relations.Count > 0)
                {
                    var lstDel = relations.Where(r => !lstGroupID.Contains(r.GroupID)).ToList();
                    if (lstDel.Count > 0)
                    {
                        _fileRelationDataService.DeleteRange(lstDel);
                        relations.RemoveAll(r => !lstGroupID.Contains(r.GroupID));
                        lstGroupID.RemoveAll(g => existingIDs.Contains(g));
                    }
                }
                var existing = relations.Select(r => r.GroupID).ToList();
                var lstInsert = lstGroupID.Where(g => !existing.Contains(g)).Select(g => new FileGroupRelation
                {
                    GroupID = g,
                    FileID = fileID
                }).ToList();
                _fileRelationDataService.AddRange(lstInsert);
                return true;
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "AddPersonGroup", ex);
                return false;
            }
        }
        [HttpPost]
        public ActionResult LoadFileGroups([DataSourceRequest]DataSourceRequest request, long fileID)
        {
            try
            {
                var qry = _fileGroupDataService.Search(new FileGroupSchema { FileID = fileID });
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadFileGroups", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Search([DataSourceRequest] DataSourceRequest request, EstateFileSchema model)
        {
            try
            {
                var qry = _fileDataService.Search(model);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("PersonController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadFileTypes()
        {
            try
            {
                var lst = new List<string>() { "اجاره", "فروش", "مشارکت", "معاوضه" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadFileTypes", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadEstateTypes()
        {
            try
            {
                var lst = new List<string>() { "آپارتمان", "ویلایی", "زمین و کلنگی", "تجاری", "مستغلات", "دفترکار(اداری)‏", "بخارجی" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadEstateTypes", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadCoolings()
        {
            try
            {
                var lst = new List<string>() { "کولر","اسپیلت", "فن کوئل", "چیلر" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadCoolings", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadHeatings()
        {
            try
            {
                var lst = new List<string>() { "موتورخانه مرکزی","اسپیلت", "پکیج", "شومینه", "بخاری", "چیلر" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadHeatings", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadBuildingFacade()
        {
            try
            {
                var lst = new List<string>() { "شمالی", "جنوبی", "شرقی", "غربی" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadBuildingFacade", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadStatus()
        {
            try
            {
                var lst = new List<string>() { "در اختبار مالک", "اجاره", "تخلیه" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadBuildingFacade", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadRestrooms()
        {
            try
            {
                var lst = new List<string>() { "ایرانی", "فرنگی", "ایرانی و فرنگی" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadRestRooms", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadInformationSource()
        {
            try
            {
                var lst = new List<string>() { "تلفنی", "حضوری", "مشاور", "فایلینگ" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadRestRooms", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadOwnershipDocumentStatus()
        {
            try
            {
                var lst = new List<string>() { "شخصی", "ستادی", "بنیادی", "اوقافی","زمین شهری" };
                return Json(new SelectList(lst.Select(l => new { ID = l, Name = l }), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "LoadOwnershipDocumentStatus", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult EstateFileOtherInfoPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult GetFile(long id)
        {
            try
            {
                var file = _fileDataService.GetEstateFile(id);
                return Json(file, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "GetFile", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult GetNextCode()
        {
            try
            {
               var code = _fileDataService.GetNextFileCode();
                return Json(code, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("EstateFileController", "GetNextCode", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }



    }
}