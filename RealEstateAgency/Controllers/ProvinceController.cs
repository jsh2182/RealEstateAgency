using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class ProvinceController : System.Web.Mvc.Controller
    {
        // GET: Province
        private readonly IProvinceDataService _provinceDataService;
        public ProvinceController(IProvinceDataService provinceDataService)
        {
            _provinceDataService = provinceDataService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Submit(Province p)
        {
            try
            {
                if (p.ProvinceID > 0)
                    _provinceDataService.Update(p);
                else
                    _provinceDataService.Add(p);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("ProvinceController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpDelete]
        public ActionResult Remove(int id)
        {
            try
            {
                var result = _provinceDataService.IsDeleteValid(id);
                if (!result)
                {
                    var msg = "امکان حذف این استان وجود ندارد" + "\n";
                    msg += "اشخاص و فایل های سیستم را بررسی کنید";
                    return Json(new { Message = msg }, JsonRequestBehavior.AllowGet);
                }
                var province = _provinceDataService.Get(id);
                _provinceDataService.Delete(province);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("ProvinceController", "Remove", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Search([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var qry = _provinceDataService.All()
                    .Select(p => new Models.Schema.ProvinceSchema { ProvinceID = p.ProvinceID, ProvinceName = p.ProvinceName });
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("ProvinceController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadProvinceList()
        {
            try
            {
                var qry = _provinceDataService.All().Select(p => new { ID = p.ProvinceID, Name = p.ProvinceName });
                return Json(new SelectList(qry, "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("ProvinceController", "LoadProvinceList", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}