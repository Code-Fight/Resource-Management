using System;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 校验
    /// </summary>
    public class CommonDBCheck
    {
        public static bool IsDBNull(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString().Trim().Length == 0)
            {
                return true;
            }

            return false;
        }

        public static int ToInt(object obj)
        {
            if (IsDBNull(obj))
            {
                return 0;
            }
            int ret = 0;
            int.TryParse(obj.ToString(), out ret);

            return ret;
        }

        public static long ToLong(object obj)
        {
            if (IsDBNull(obj))
            {
                return 0;
            }
            return long.Parse(obj.ToString());
        }

        public static DataTable ToDataTable(object obj)
        {
            if (IsDBNull(obj))
            {
                return null;
            }
            
            return (DataTable)obj;
        }

        public static DateTime ToDateTime(object obj)
        {
            if (IsDBNull(obj))
            {
                return DateTime.MinValue;
            }

            return DateTime.Parse(obj.ToString());
        }

        public static float ToFloat(object obj)
        {
            if (IsDBNull(obj))
            {
                return 0.0F;
            }
            float ret;
            float.TryParse(obj.ToString(), out ret);
            return ret;
        }

        public static double ToDouble(object obj)
        {
            if (IsDBNull(obj))
            {
                return 0.0F;
            }
            double ret;
            double.TryParse(obj.ToString(), out ret);
            return ret;
        }

        public static byte[] ToBytes(object obj)
        {
            if (IsDBNull(obj))
            {
                return null;
            }
            return (byte[])obj;
        }

        public static string ToString(object obj)
        {
            if (IsDBNull(obj))
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        public static decimal Todecimal(object obj)
        {
            if (IsDBNull(obj))
            {
                return 0;
            }
            return decimal.Parse(obj.ToString());
        }

        public static bool ToBool(object obj)
        {
            if (IsDBNull(obj))
            {
                return false;
            }
            return bool.Parse(obj.ToString());
        }
    }
}
