using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class AttachmentController : System.Web.Mvc.Controller
    {
        // GET: Attachment
        DataService.IAttachmentDataService _attachmentDataService;
        public AttachmentController(DataService.IAttachmentDataService attachmentDataService)
        {
            _attachmentDataService = attachmentDataService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload(HttpPostedFileBase uploadFile, string parentIDAndAttachType)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        byte[] bytes = new byte[uploadFile.InputStream.Length + 1];
                        uploadFile.InputStream.Read(bytes, 0, bytes.Length);
                        var fileName = parentIDAndAttachType + "_" + uploadFile.FileName;
                        var fileContent = new ByteArrayContent(bytes);
                        fileContent.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("form-data") { Name = "FileToUpload", FileName = fileName };
                        formData.Add(fileContent);
                        var request = System.Web.HttpContext.Current.Request;
                        var appUrl = HttpRuntime.AppDomainAppVirtualPath;
                        if (appUrl != "/")
                            appUrl = "/" + appUrl;
                        var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
                        var response = client.PostAsync(baseUrl + "api/AttachFile", formData).Result;
                        if (response.IsSuccessStatusCode)
                            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
                        else
                            return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

                Tools.Add2Log("AttachmemtController", "Upload", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
            finally
            {

            }
        }
        [HttpDelete]
        public ActionResult Delete(string fileName)
        {

            try
            {
                var fName = Path.GetFileName(fileName);
                var deletePath = Path.Combine(Server.MapPath("~/Resources/Attachments/"), fName);


                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                   var file= _attachmentDataService.All().FirstOrDefault(a => a.AttachmentName == fileName);
                    _attachmentDataService.Delete(file);
                }
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("AttachmentController", "Delete", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AttachmentPartial(string parentIDAndAttachType)
        {
            ViewBag.ParentIDAndAttachType = parentIDAndAttachType;
            if(parentIDAndAttachType != null)
                ViewBag.AttachType = parentIDAndAttachType.Split('_')[1];
            return PartialView();
        }
        public ActionResult FileTransferPartial(long fileID)
        {
            try
            {
                ViewBag.FileID = fileID;
                return PartialView();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult LoadAttachments(Kendo.Mvc.UI.DataSourceRequest request, long parentID, string attachType)
        {
            try
            {
                var qry = _attachmentDataService.Search(new Models.Schema.AttachementSchema()
                {
                    ParentID = parentID,
                    AttachmentType = attachType
                });
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("AttachmentController", "LoadAttachments", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}