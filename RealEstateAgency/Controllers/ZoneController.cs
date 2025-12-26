using RealEstateAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateAgency.DataService;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace RealEstateAgency.Controllers
{
    public class ZoneController : System.Web.Mvc.Controller
    {
        // GET: Zone
        ICityDataService _cityDataService;
        IZoneDataService _zoneDataService;
        public ZoneController(ICityDataService cityDataService, IZoneDataService zoneDataService)
        {
            _cityDataService = cityDataService;
            _zoneDataService = zoneDataService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(Zone z)
        {
            try
            {
                if (z.ZoneID > 0)
                    _zoneDataService.Update(z);
                else
                    _zoneDataService.Add(z);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("ZoneController", "Submit", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpDelete]
        public ActionResult Remove(int id)
        {
            try
            {
                var result = _zoneDataService.IsDeleteValid(id);
                if (!result)
                {
                    var msg = "امکان حذف این مورد وجود ندارد" + "\n";
                    msg += "اشخاص و فایل ها را بررسی کنید";

                    return Json(new { Message = msg }, JsonRequestBehavior.AllowGet);
                }
                var zone = _zoneDataService.Get(id);
                _zoneDataService.Delete(zone);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("ZoneController", "Remove", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Search([DataSourceRequest]DataSourceRequest request, int cityID)
        {
            try
            {
                var qry = _zoneDataService.Search(cityID);
                return Json(qry.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("CityController", "Search", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadZoneList(int cityID)
        {
            try
            {
                var qry = _zoneDataService.All().Where(z => z.CityID == cityID).Select(c => new { ID = c.ZoneID, Name = c.ZoneName });
                return Json(new SelectList(qry, "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("ZoneController", "LoadZoneList", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
