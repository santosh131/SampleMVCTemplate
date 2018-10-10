using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using SampleHelpers;

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
            IConnectionFactory cf = new CustomConnectionFactory();
            _customConnection = cf.GetConnection(CommonEnums.DataBases.Sql);
            _customConnection.SetConnectionObject(ConnectionName);
        }
    }
}
