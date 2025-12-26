using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstateAgency.DataService;
using RealEstateAgency.Models.Schema;

namespace RealEstateAgency.Controllers
{
    public class FileReferAndRequestController : System.Web.Mvc.Controller
    {
        private IFileReferDataService _fileReferDataService;
        IFileRequestDataService _fileRequestDataService;

        // GET: FileRefer

        public FileReferAndRequestController(IFileReferDataService fileReferDataService, IFileRequestDataService fileRequestDataService)
        {
            _fileReferDataService = fileReferDataService;
            _fileRequestDataService = fileRequestDataService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FileReferPartial()
        {
            return PartialView();
        }
        public ActionResult FileRequestPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SearchRefer(FileReferSchema model, DataSourceRequest request)
        {
            try
            {
                var qry = _fileReferDataService.Search(model);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileReferController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SubmitRefer(FileReferSchema model)
        {
            try
            {
                if (!model.RequestID.HasValue || model.RequestID == 0)
                    return Json(HttpStatusCode.Forbidden, JsonRequestBehavior.AllowGet);
                if (model.FileReferID > 0)
                    _fileReferDataService.UpdateRefer(model);
                else
                    _fileReferDataService.AddRefer(model);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileReerController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteRefer(long referID)
        {
            try
            {
                if (Tools.CurrentUser == null)
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                _fileReferDataService.DeleteRefer(referID);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileReferController", "Delete", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SearchRequest(FileRequestSchema model, DataSourceRequest request)
        {
            try
            {
                var qry = _fileRequestDataService.Search(model);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileRequestController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SubmitRequest(FileRequestSchema model)
        {
            try
            {
                if (model.FileRequestID > 0)
                    _fileRequestDataService.UpdateRequest(model);
                else if (!_fileRequestDataService.RequestExists(Tools.CurrentUser.UserID, model.FileID))
                    _fileRequestDataService.AddRequest(model);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileReerController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpDelete]
        public ActionResult DeleteRequest(long RequestID)
        {
            try
            {
                if (Tools.CurrentUser == null)
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                var result = _fileRequestDataService.RemoveRequest(RequestID);
                HttpStatusCode status ;
                if (result == Enums.RemoveResult.Success)
                    status = HttpStatusCode.OK;
                else if (result == Enums.RemoveResult.DeleteIsNotValid)
                    status = HttpStatusCode.Forbidden;
                else
                    status = HttpStatusCode.BadRequest;
                return Json(status, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {

                Tools.Add2Log("FileRequestController", "Delete", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}