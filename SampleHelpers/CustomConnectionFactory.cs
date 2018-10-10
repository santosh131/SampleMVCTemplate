using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace SampleHelpers
{
    public class CustomConnectionFactory : IConnectionFactory
    {
        public ICustomConnection GetConnection(string connString, CommonEnums.DataBases ce)
        {
            if (ce == CommonEnums.DataBases.Sql)
            {
                return new CustomSqlConnection(connString);
            }
            else
                return null;
        }

        public ICustomConnection GetConnection(CommonEnums.DataBases ce)
        {
            if (ce == CommonEnums.DataBases.Sql)
            {
                return new CustomSqlConnection();
            }
            else
                return null;
        }
    }
}
