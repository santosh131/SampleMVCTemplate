using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class DatabaseColumnName: Attribute
    {
        private string _col_name;

        public string ColumnName
        {
            get { return _col_name; }
            set { _col_name = value; }
        }
    }
}
