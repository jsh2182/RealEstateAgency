using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateAgency.DataService;

namespace RealEstateAgency.Controllers
{
    public class PageListController : System.Web.Mvc.Controller
    {
        private readonly IPageListDataService _pageListService;

        public PageListController(IPageListDataService PageListService)
        {
            this._pageListService = PageListService;
        }

        [HttpGet]
        public ActionResult TreeViewPageListPartial()
        {
            return PartialView();
        }

        public JsonResult TreeViewPageList(int? id = null)
        {
            var Query = _pageListService.SelectTreeView(id);

            return Json(Query, JsonRequestBehavior.AllowGet);
        }
    }
}