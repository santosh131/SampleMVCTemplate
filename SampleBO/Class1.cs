using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using SampleModels;
using System.Data;

namespace SampleBO
{
   public class Class1
    {
        public void Func1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            DataRow drdt = dt.NewRow();
            drdt[0] = 1;
            drdt[1] = "Sam";
            dt.Rows.Add(drdt);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            IDataReader rdr = null;
            rdr = ds.CreateDataReader(); 
            List<Model1> x = new List<Model1>();
            x = ConversionHelpers.ConvertToList<Model1>(rdr);
        }
    }
}
