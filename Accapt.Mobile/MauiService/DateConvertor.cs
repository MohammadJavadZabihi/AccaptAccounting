using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Mobile.MauiService
{
    public class DateConvertor
    {
        public static string ConvertToShamsi(DateTime time)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int persianYear = persianCalendar.GetYear(time);
            int persianMonth = persianCalendar.GetMonth(time);
            int persianDay = persianCalendar.GetDayOfMonth(time);

            string shamsiYear = $"{persianYear:0000}/{persianMonth:00}/{persianDay:00}";

            return shamsiYear;
        }
    }
}
