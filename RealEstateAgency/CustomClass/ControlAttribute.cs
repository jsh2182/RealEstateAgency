using Ninject;
using RealEstateAgency.Common;
using System.Linq;
using System.Web.Mvc;

using System.Web.Routing;

namespace RealEstateAgency
{
    public class ControlAttribute : ActionFilterAttribute, IActionFilter
    {
        private string PageName;
        private string _actionType;
        private DataService.IPageListDataService pageService;
        //public ControlAttribute()
        //{

        //}
        public ControlAttribute(string PageName, string actionType)
        {
            this.PageName = PageName;
            this.pageService = App_Start.NinjectWebCommon.Kernel.Get<DataService.IPageListDataService>();
            _actionType = actionType;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Tools.CurrentUser != null)
            {
                var permissions = pageService.All().Where(o => o.Name == PageName && o.PageListPermissions.Any(p => p.UserID == Tools.CurrentUser.UserID))
                                                    .Select(o => o.PageListPermissions).FirstOrDefault();
                if (permissions != null && permissions.Count > 0)
                {
                    if (permissions.Any())
                    {
                        if (permissions.Any(o => o.UserID == Tools.CurrentUser.UserID &&
                                                ((o.IsRead == true && _actionType == "read") ||
                                                   (o.IsDelete == true && _actionType == "delete") ||
                                                   (o.IsSave == true && _actionType == "insert") ||
                                                   (o.IsUpdate == true && _actionType == "update")
                                                )
                                                 ))//.Any())
                        {
                            base.OnActionExecuting(filterContext);
                        }

                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary {{ "Controller", "Account" },
                                      { "Action", "NoAccess" }  });
                        }

                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {{ "Controller", "Account" },
                                      { "Action", "NoAccess" }  });
                }
            }
            else
            {
                var dictionary = new RouteValueDictionary
                {
                    ["action"] = "Login",
                    ["controller"] = "Account"
                    //["returnUrl"] = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri
                };
                filterContext.Result =
               new RedirectToRouteResult(dictionary);
                //new RouteValueDictionary(new { Controller = "Account", Action = "Login" }));
                //  ,{ "returnUrl","/"+filterContext.Controller+"/"+filterContext.ActionDescriptor.ActionName}});
            }


        }
    }
}
