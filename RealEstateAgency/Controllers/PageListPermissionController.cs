using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class PageListPermissionController : System.Web.Mvc.Controller
    {
        private IPageListPermissionDataService _pageListPermissionService;
        private IPageListDataService _pageListService;

        public PageListPermissionController(IPageListPermissionDataService PageListPermissionService, IPageListDataService pageListService)
        {
            _pageListPermissionService = PageListPermissionService;
            _pageListService = pageListService;
        }

        [HttpPost]
        public JsonResult SelectPageListPermission(int userID, int pageListID)
        {
            try
            {
                var qry = _pageListPermissionService
                                .Search(new PageListPermissionSchema()
                                {
                                    UserID = userID,
                                    PageListID = pageListID
                                }).FirstOrDefault();

                if (qry == null)
                {
                    return Json("null", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(qry, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddPermission(PageListPermission plp)
        {
            try
            {
                if (Tools.CurrentUser == null)
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                //plp.UserID = Tools.CurrentUser.UserID;
                plp.CreateDate = DateTime.Now;

                _pageListPermissionService.UpdateAndSave(plp, Tools.CurrentUser.UserID, plp.PersonID);
                Common.GlobalItem.PageList = _pageListService.All().Include(o => o.PageListPermissions).ToList();

                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tools.Add2Log("PageListPermissionController", "AddPermission", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
                
            }
        }
    }
}