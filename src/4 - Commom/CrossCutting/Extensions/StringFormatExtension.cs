using System;
using System.Globalization;

namespace CrossCutting.Extensions
{
    public static class StringFormatExtension
    {
        public static bool IsValidDateFormat(this string input, string dateFormat = "yyyy-MM-dd")
        {
            try
            {
                DateTime.ParseExact(input, dateFormat, CultureInfo.InvariantCulture);
            
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }


}
