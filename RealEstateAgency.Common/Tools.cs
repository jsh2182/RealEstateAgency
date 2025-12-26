using RealEstateAgency.Common;
using RealEstateAgency.Models;
using RealEstateAgency.Models.Schema.Coockie;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace RealEstateAgency
{
    public static class Tools
    {
        #region HandleDate

        public static string ToPersian(this DateTime date)
        {
            System.Globalization.PersianCalendar g = new System.Globalization.PersianCalendar();
            return string.Format("{0}/{1}/{2}", g.GetYear(date), g.GetMonth(date).ToString("00"), g.GetDayOfMonth(date).ToString("00"));
        }

        public static DateTime ConvertPersionDate(this string date)
        {
            if (date != null)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(date, @"\d{4}/\d{2}/\d{2}"))
                {
                    string[] splitted = date.Split('/');
                    System.Globalization.PersianCalendar g = new System.Globalization.PersianCalendar();

                    return g.ToDateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]), 0, 0, 0, 0);
                }

                else if (System.Text.RegularExpressions.Regex.IsMatch(date, @"\d{2}/\d{2}/\d{4}"))
                {
                    string[] splitted = date.Split('/');
                    System.Globalization.PersianCalendar g = new System.Globalization.PersianCalendar();

                    return g.ToDateTime(int.Parse(splitted[2].Substring(0, 4)), int.Parse(splitted[0]), int.Parse(splitted[1]), 0, 0, 0, 0);
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(date, @"\d{4}/\d{1}/\d{2}"))
                {
                    string[] splitted = date.Split('/');
                    System.Globalization.PersianCalendar g = new System.Globalization.PersianCalendar();

                    return g.ToDateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]), 0, 0, 0, 0);
                }

                else if (System.Text.RegularExpressions.Regex.IsMatch(date, @"\d{4}/\d{2}/\d{1}"))
                {
                    string[] splitted = date.Split('/');
                    System.Globalization.PersianCalendar g = new System.Globalization.PersianCalendar();

                    return g.ToDateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]), 0, 0, 0, 0);
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(date, @"\d{4}/\d{1}/\d{1}"))
                {
                    string[] splitted = date.Split('/');
                    System.Globalization.PersianCalendar g = new System.Globalization.PersianCalendar();

                    return g.ToDateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]), 0, 0, 0, 0);
                }

            }

            return DateTime.MinValue;
        }

        public static string PersianDate(string test)
        {

            System.Globalization.PersianCalendar oPersianC = new System.Globalization.PersianCalendar();
            string Day, Month, Year, Date = "";

            if (string.IsNullOrEmpty(test))
            {
                Year = oPersianC.GetYear(System.DateTime.Now).ToString();
                Month = oPersianC.GetMonth(System.DateTime.Now).ToString();
                Day = oPersianC.GetDayOfMonth(System.DateTime.Now).ToString();
                string day = DateTime.Now.DayOfWeek.ToString();
                switch (day)
                {
                    case "Sunday":
                        day = "یکشنبه";
                        break;
                    case "Monday":
                        day = "دوشنبه";
                        break;
                    case "Tuesday":
                        day = "سه شنبه";
                        break;
                    case "Wednesday":
                        day = "چهارشنبه";
                        break;
                    case "Thursday":
                        day = "پنج شنبه";
                        break;
                    case "Friday":
                        day = "جمعه";
                        break;
                    case "Saturday":
                        day = "شنبه";
                        break;

                }
                switch (Month)
                {
                    case "1":
                        Month = "فروردین";
                        break;
                    case "2":
                        Month = "اردیبهشت";
                        break;
                    case "3":
                        Month = "خرداد";
                        break;
                    case "4":
                        Month = "تیر";
                        break;
                    case "5":
                        Month = "مرداد";
                        break;
                    case "6":
                        Month = "شهریور";
                        break;
                    case "7":
                        Month = "مهر";
                        break;
                    case "8":
                        Month = "آبان";
                        break;
                    case "9":
                        Month = "آذر";
                        break;
                    case "10":
                        Month = "دی";
                        break;
                    case "11":
                        Month = "بهمن";
                        break;
                    case "12":
                        Month = "اسفند";
                        break;
                    default:
                        break;
                }
                Date = day + " " + Day + "  " + Month + "  " + Year;
            }
            else
            {
            }
            return Date;
        }

        public static string Persiantime()
        {
            string day = DateTime.Now.DayOfWeek.ToString();
            switch (day)
            {
                case "Sunday":
                    day = "یکشنبه";
                    break;
                case "Monday":
                    day = "دوشنبه";
                    break;
                case "Tuesday":
                    day = "سه شنبه";
                    break;
                case "Wednesday":
                    day = "چهارشنبه";
                    break;
                case "Thursday":
                    day = "پنج شنبه";
                    break;
                case "Friday":
                    day = "جمعه";
                    break;
                case "Saturday":
                    day = "شنبه";
                    break;

            }

            string now = DateTime.Now.ToShortTimeString();
            if (now.Contains("PM"))
            {
                now = now.Replace("PM", "بعد از ظهر");
            }
            if (now.Contains("AM"))
            {
                now = now.Replace("PM", "صبح");
            }
            return day + "  " + now;
        }

        #endregion

        public static string GetLayoutName()
        {
            //string viewFile = string.Format("~/Views/Shared/{0}/_Layout.cshtml", ThemeName);
            var viewFile = "~/Views/Shared/_Layout.cshtml";
            string absFile = HttpContext.Current.Server.MapPath(viewFile);
           // if (System.IO.File.Exists(absFile))
            {
                return viewFile;
            }
            //else
            //{
            //    return string.Format("~/Views/Shared/{0}", "_Layout.cshtml");
            //}
        }
        public static User CurrentUser
        {

            get
            {
                if (SetCookie.IsActiveCookie(new Coockie { RealEstateAgency = CoockieEnum.ManagementRealEstateAgencyCMS }))
                {
                    var coockie = SetCookie.ValueCookie(new Coockie { RealEstateAgency = CoockieEnum.ManagementRealEstateAgencyCMS });

                    using (AgencyContext db = new AgencyContext())
                    {
                        AuthToken tokenID = db.AuthTokens.OrderByDescending(o => o.TokenID).Where(o => o.Token == coockie.ActiveCode).FirstOrDefault();
                        if (tokenID != null)
                        {
                            return tokenID.User;
                        }
                    }
                }
                if (HttpContext.Current.Request.Headers.AllKeys.Contains("Token"))
                {
                    using (AgencyContext db = new AgencyContext())
                    {
                        string token = HttpContext.Current.Request.Headers.GetValues("Token").FirstOrDefault();
                        string tokenDecrypted = Cryptography.RC2Decryption(token, Cryptography.cipherKey);

                        AuthToken tokenID = db.AuthTokens.OrderByDescending(o => o.TokenID).Where(o => o.Token == tokenDecrypted).FirstOrDefault();
                        if (tokenID != null)
                        {
                            return tokenID.User;
                        }
                    }
                }

                return null;
            }
        }
        public static void Add2Log(string formName, string methodName, Exception ex)
        {
            try
            {
                using (AgencyContext db = new AgencyContext())
                {
                    string msg = "";
                    if (ex.GetType().Name == "DbEntityValidationException")
                    {
                        var exp = (DbEntityValidationException)ex;
                        foreach (var validationErrors in exp.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                msg += "Prop: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage + "\n";
                            }
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (!ex.Message.Contains("See the inner exception for details"))
                            {
                                msg += ex.Message + "\n" + ex.StackTrace + "\n";
                            }

                            if (ex.InnerException == null)
                            {
                                break;
                            }

                            ex = ex.InnerException;
                        }
                    }
                    db.ExceptionLogs.Add(new ExceptionLog
                    {
                        FormName = formName,
                        MethodName = methodName,
                        ExcpetionDesc = msg.Replace("The statement has been terminated.", ""),
                        ExceptionDate = DateTime.Now,

                    });

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
