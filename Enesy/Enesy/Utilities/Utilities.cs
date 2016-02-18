using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Geometry;

namespace Enesy
{
    public class Utilities
    {
        /// <summary>
        /// Determines whether the specified
        /// value can be converted to a valid number.
        /// From: CSLA code
        /// </summary>
        public static bool IsNumeric(object value)
        {
            double dbl;
            return double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any,
              System.Globalization.NumberFormatInfo.InvariantInfo, out dbl);
        }

        /// <summary>
        /// Trim all Space and Tab char in string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimSpaceAndTab(string value)
        {
            char tab = '\u0009';
            value = value.Replace(" ", "");            
            return value.Replace(tab.ToString(), "");
        }
    }
}
