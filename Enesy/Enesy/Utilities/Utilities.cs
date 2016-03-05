using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Enesy
{
    public static class Utilities
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
        /// Determines whether the specified value can be converted to valid number larger 0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumericLargerZero(object value)
        {
            if (!IsNumeric(value)) return false;
            double dbl;
            dbl = double.Parse(value.ToString());
            return (dbl > 0) ? true : false;
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

        /// <summary>
        /// Create filter expression for dataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CreateExpression(DataTable dt, string colname, string value)
        {
            if (value == "") return "";

            string expression = null;

            if (colname != null && dt.Columns[colname] != null)
            {
                if ("Byte,Decimal,Double,Int16,Int32,Int64,SByte,Single,UInt16,UInt32,UInt64,".Contains(dt.Columns[colname].DataType.Name + ","))
                {
                    expression = colname + "=" + value;
                }
                else if (dt.Columns[colname].DataType == typeof(string))
                {
                    expression = string.Format(colname + " LIKE '%{0}%'", value);
                }
                else if (dt.Columns[colname].DataType == typeof(DateTime))
                {
                    expression = colname + " = #" + value + "#";
                }
            }

            return expression;
        }
    }
}
