using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using SampleHelpers;
using System.Configuration;

namespace SampleDAL
{
    public class DataAccessObject
    {
        public const string ConnectionName ="DefaultConnection";

        private ICustomConnection _customConnection;
        public ICustomConnection CustomConnection
        {
            get { return _customConnection; }
        }

        public DataAccessObject()
        {
            string connString = ConfigurationSettings.AppSettings["DefaultConnection"].ToString();
            IConnectionFactory cf = new CustomConnectionFactory();
            _customConnection = cf.GetConnection(CommonEnums.DataBases.Sql);
            _customConnection.SetConnectionObject(connString);
        }
    }
}
