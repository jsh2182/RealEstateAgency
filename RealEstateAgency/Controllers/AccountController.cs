using RealEstateAgency.Common;
using RealEstateAgency.DataService;
using RealEstateAgency.Models.Schema;
using RealEstateAgency.Models.Schema.Coockie;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RealEstateAgency.Controllers
{
    public class AccountController : System.Web.Mvc.Controller
    {
        private readonly IUserDataService _userService;
        private readonly IAuthTokenDataService _authTokenService;

        public AccountController(IUserDataService UserService,
            IAuthTokenDataService authTokenService)
        {
            this._userService = UserService;
            this._authTokenService = authTokenService;
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************
        public ActionResult LogOn(string returnUrl)
        {
            if (Tools.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NoAccess");
            }

            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnSchema model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByName(model.UserName);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            }

            return View(model);
        }

        public ActionResult PartialLogOn(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NoAccess");
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult PartialLogOn(LogOnSchema model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByName(model.UserName);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Json(new { redirectTo = returnUrl });
                }
                else
                {
                    return Json(new { redirectTo = Url.Action("Index", "Home") });
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            }

            // If we got this far, something failed, redisplay form
            TempData["errorLogon"] = "نام کاربری یا رمز عبور اشتباه است";
            return PartialView();
        }
        [HttpGet]
        public ActionResult Login()
        {
            SetCookie.DeleteCookie(new Coockie { RealEstateAgency = CoockieEnum.ManagementRealEstateAgencyCMS });
            return View();
        }



        [HttpPost]
        public ActionResult Login(LogOnSchema model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var password = Cryptography.RC2Encryption(model.Password, Cryptography.cipherKey);
                var exists = _userService.All().Where(w => w.UserName == model.UserName && w.IsActive && w.Password == password).FirstOrDefault();
                if (exists != null)
                {
                    var AutoToken = Guid.NewGuid();
                    var dt = DateTime.Now;

                    _authTokenService.Add(new Models.AuthToken
                    {
                        Token = AutoToken.ToString(),
                        ExpiresOn = dt.AddHours(4),
                        IssuedOn = dt,
                        UserID = exists.UserID
                    });

                    SetCookie.AddCookie(new Coockie
                    {
                        ActiveCode = AutoToken.ToString(),
                        RealEstateAgency = CoockieEnum.ManagementRealEstateAgencyCMS,
                        RememberMe = true
                    });
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    return View(model);
            }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View(model);
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult SuccessfullySend()
        {

            return View();
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult NoAccess()
        {
            return View();
        }

        public ActionResult LogOn(Uri returnUrl)
        {
            throw new NotImplementedException();
        }

        public ActionResult LogOn(LogOnSchema model, Uri returnUrl)
        {
            throw new NotImplementedException();
        }

        public ActionResult PartialLogOn(Uri returnUrl)
        {
            throw new NotImplementedException();
        }
    }
}