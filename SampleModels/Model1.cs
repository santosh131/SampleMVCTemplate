using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace SampleModels
{
    public class Model1 : ModelHelpers
    {
        private int _id;

        [DatabaseColumnName(ColumnName = "Id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [DatabaseColumnName(ColumnName = "Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}
