using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Models.Helper
{
    public static class DateHelper
    {
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
    }
}
