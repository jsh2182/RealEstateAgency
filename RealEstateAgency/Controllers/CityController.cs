using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateAgency.Models;
using RealEstateAgency.DataService;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace RealEstateAgency.Controllers
{
    public class CityController : System.Web.Mvc.Controller
    {
        // GET: City
        ICityDataService _cityDataService;
        IProvinceDataService _provinceDataService;
        public CityController(ICityDataService cityDataService, IProvinceDataService provinceDataService)
        {
            _cityDataService = cityDataService;
            _provinceDataService = provinceDataService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Submit(City c)
        {
            try
            {
                if (c.CityID > 0)
                    _cityDataService.Update(c);
                else
                    _cityDataService.Add(c);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("CityController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpDelete]
        public ActionResult Remove(int id)
        {
            try
            {
                var result = _cityDataService.IsDeleteValid(id);
                if (!result)
                {
                    var msg = "امکان حذف این مورد وجود ندارد" + "\n";
                    msg += "اشخاص و فایل ها را بررسی کنید";

                    return Json(new { Message = msg }, JsonRequestBehavior.AllowGet);
                }
                var city = _cityDataService.Get(id);
                _cityDataService.Delete(city);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("CityController", "Remove", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Search([DataSourceRequest]DataSourceRequest request, int provinceId)
        {
            try
            {
                var qry = _cityDataService.Search(provinceId);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("CityController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadCityList(int? provinceID)
        {
            try
            {
                var qry = _cityDataService.All().Where(c => (provinceID == null || c.ProvinceID == provinceID))
                                            .Select(c=> new { ID = c.CityID, Name = c.CityName });
                return Json(new SelectList(qry, "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("CityController", "LoadCityList", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }

    }
}