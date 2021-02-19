using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class ExtensionsHelpers
    {
        public static bool IsColumnExists(this IDataReader dr, string columnName)
        {
            if (dr != null)
                return dr.GetSchemaTable().Rows.OfType<DataRow>().Any(row => row["ColumnName"].ToString() == columnName);
            else
                return false;
        }

        public static int ToInteger(this string val)
        {
            if (!string.IsNullOrEmpty(val) && ValidationHelpers.IsInteger(val))
                return int.Parse(val);
            return 0;
        }

        public static Int64 ToInteger64(this string val)
        {
            if (!string.IsNullOrEmpty(val) && ValidationHelpers.IsInteger64(val))
                return Int64.Parse(val);
            return 0;
        }

        public static object ToBoolean(this object val, bool isNullable = false)
        {
            if (val != null)
            {
                if (ValidationHelpers.IsBool(val.ToString()))
                    return bool.Parse(val.ToString());
            }
            if (isNullable)
                return DBNull.Value;

            return false;
        }

        public static object ToDateTime(this object val, bool isNullable = false)
        {
            if (val != null)
            {
                if (ValidationHelpers.IsDate(val.ToString()) && DateTime.Parse(val.ToString()) != DateTime.MinValue)
                {
                    return DateTime.Parse(val.ToString());
                }
            }
            if (isNullable)
                return DBNull.Value;
            return val;
        }
    }
}
