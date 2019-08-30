using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsSameYear(this DateTime datetime1, DateTime datetime2)
        {
            return datetime1.Year == datetime2.Year;
        }
        public static bool IsSameMonth(this DateTime datetime1, DateTime datetime2)
        {
            return datetime1.IsSameYear(datetime2)
                && datetime1.Month == datetime2.Month;
        }

        public static bool IsSameDay(this DateTime datetime1, DateTime datetime2)
        {
            return datetime1.IsSameMonth(datetime2)
                && datetime1.Day == datetime2.Day;
        }
    }
}
