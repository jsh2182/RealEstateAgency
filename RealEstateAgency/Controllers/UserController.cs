using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstateAgency.DataService;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class UserController : System.Web.Mvc.Controller
    {
        // GET: User
        IUserDataService _userDataService;
        public UserController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }
        [Control("User","read")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadUserList()
        {
            try
            {
                var qry = _userDataService.Search(new UserSchema { IsActive = true }).Select(q => new { ID = q.UserID, q.Name });
                return Json(new SelectList(qry, "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Tools.Add2Log("UserController", "LoadUserList", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ListUser([DataSourceRequest]DataSourceRequest command, User user)
        {
            IQueryable<User> Query = _userDataService.All();

            if (!string.IsNullOrEmpty(user.UserName))
            {
                Query = Query.Where(w => w.UserName == user.UserName);
            }

            IQueryable<UserSchema> QuerySelect = Query.Select(s => new UserSchema
            {
                UserID = s.UserID,
                IsActive = s.IsActive,
                Name = s.Name,
                UserName = s.UserName,
                Password = "#J$a%V&a@d",
                IsAdmin = s.IsAdmin
            }).AsQueryable();

            return Json(QuerySelect.ToDataSourceResult(command), JsonRequestBehavior.AllowGet);
        }
        [Control("User","update")]
        [HttpPost]
        public ActionResult SubmitUser(UserSchema model)
        {
            try
            {
                User user;
                if (model.UserID > 0)
                {
                    user = _userDataService.Get(model.UserID);
                }
                else
                    user = new User();
                user.UserName = model.UserName;
                if (model.IsActive == null)
                    user.IsActive = false;
                else
                    user.IsActive = (bool)model.IsActive;
                user.Name = model.Name;
                user.IsAdmin = model.IsAdmin;
                if (model.Password != "#J$a%V&a@d")
                    user.Password = Cryptography.RC2Encryption(model.Password, Cryptography.cipherKey);
                if (model.UserID > 0)
                    _userDataService.Update(user);
                else
                _userDataService.Add(user);
                return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                Tools.Add2Log("UserController", "SubmitUser", ex);
                return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
            }
        }
    }
}