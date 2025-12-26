using RealEstateAgency.Models.Schema.Coockie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RealEstateAgency.Common
{
    public static class SetCookie
    {
        private static string ActiveCode
        {
            get
            {
                return new EncodeAndDecode().Encode(CoockieEnum.ActiveCode.ToString());
            }
        }

        public static void AddCookie(Coockie ck)
        {
            EncodeAndDecode ead = new EncodeAndDecode();
            HttpCookie CoockieValue = new HttpCookie(ead.Encode(ck.RealEstateAgency.ToString()));

            if (ck.RememberMe)
                CoockieValue.Expires = DateTime.Now.AddHours(4);

            CoockieValue.Values[ActiveCode] = ead.Encode(ck.ActiveCode);

            HttpContext.Current.Response.Cookies.Add(CoockieValue);
        }

        public static Coockie ValueCookie(Coockie ck)
        {
            EncodeAndDecode ead = new EncodeAndDecode();

            if (HttpContext.Current.Request.Cookies[ead.Encode(ck.RealEstateAgency.ToString())] != null)
            {
                ck.ActiveCode = ead.Decode(HttpContext.Current.Request.Cookies[ead.Encode(ck.RealEstateAgency.ToString())][ActiveCode]);

                return ck;
            }
            else
                return null;
        }

        public static void DeleteCookie(Coockie ck)
        {
            EncodeAndDecode ead = new EncodeAndDecode();

            if (HttpContext.Current.Request.Cookies[ead.Encode(ck.RealEstateAgency.ToString())] != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[ead.Encode(ck.RealEstateAgency.ToString())];

                cookie.Expires = DateTime.Now.AddDays(-1);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }

        public static bool IsActiveCookie(Coockie ck)
        {
            EncodeAndDecode ead = new EncodeAndDecode();

            if (HttpContext.Current.Request.Cookies[ead.Encode(ck.RealEstateAgency.ToString())] != null)
                return true;
            else
                return false;
        }
    }
}
