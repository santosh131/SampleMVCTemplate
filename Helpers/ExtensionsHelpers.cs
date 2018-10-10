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
        public static bool IsColumnExists(this IDataReader dr,string columnName)
        {
            if (dr != null)
                return dr.GetSchemaTable().Columns.Contains(columnName);
            else
                return false;
        }
       

    }
}
