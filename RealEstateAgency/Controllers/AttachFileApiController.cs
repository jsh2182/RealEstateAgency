using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RealEstateAgency.DataService;

namespace RealEstateAgency.Controllers
{
    public class AttachFileApiController : ApiController
    {
        // GET: Attachment
        IAttachmentDataService _attachmentDataService;
        public AttachFileApiController(IAttachmentDataService attachmentDataService)
        {
            _attachmentDataService = attachmentDataService;
        }
        [System.Web.Http.Route("api/AttachFile")]
        public JsonResult Post()
        {
            try
            {
                var request = HttpContext.Current.Request;
                if (request.Files.Count > 0)
                {
                    foreach (string file in request.Files)
                    {

                        var postedFile = request.Files[file];
                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        var path = HttpContext.Current.Server.MapPath("~/Resources/Attachments/") + fileName;
                        var array = fileName.Split('_');                        
                        long parentID = long.Parse(array[0]);
                        var attachmentType = array[1];
                        int userID = int.Parse(array[2]);
                        string name = array[3];
                        _attachmentDataService.Add(new Models.Attachment
                        {
                            AttachDate = DateTime.Now,
                            AttachedBy = userID,
                            AttachmentName = fileName,
                            AttachmentType = attachmentType,
                            IsDeleted = false,
                            ParentID = parentID
                            
                        });
                        postedFile.SaveAs(path);
                    }
                }
                return new JsonResult() { Data = new { Message = "Success" } };
            }
            catch (Exception ex)
            {

                Tools.Add2Log("AttachmentApiController", "Post", ex);
                return new JsonResult() { Data = new { Message = "Error" } };
            }
        }
    }
}