using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CashOverflow.Utilities.Extensions
{
    public static class IntegerExtensions
    {
        public static string FormatCurrency(this decimal number)
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

            nfi.NumberGroupSeparator = " ";

            return number.ToString("#,0.00", nfi);
        }

        public static string FormatCurrency(this decimal number, string currency)
        {
            return number.FormatCurrency() + " " + currency;
        }
    }
}
