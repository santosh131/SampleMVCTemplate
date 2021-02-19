using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHelpers
{
    public static class TableTypeHelpers
    {
        public static DataTable GetMenuActionTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("menu_action_id", typeof(string));
            dt.Columns.Add("selected_ind",typeof(bool));
            return dt;
        }
    }
}
