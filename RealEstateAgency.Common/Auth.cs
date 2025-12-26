using System.Web;
using System.Web.Mvc;

namespace RealEstateAgency
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public string AuthType { get; set; }

        public AuthAttribute()
        {
        }

        public AuthAttribute(string authType)
        {
            AuthType = authType;
        }

        public AuthAttribute(string authType, int order)
        {
            this.Order = order;
            AuthType = authType;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);
        }
    }

    public enum AuthTypes
    {
        BookLibrary
    }
}