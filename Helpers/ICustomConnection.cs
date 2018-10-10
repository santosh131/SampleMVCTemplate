using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface ICustomConnection
    {
        void SetConnectionObject(string connString);
        void AddTableParameters(string name, SqlDbType dbType, ParameterDirection paramDirection, int size, object value);
        void AddParameters(string name, DbType dbType, ParameterDirection paramDirection, int size, object value);
        DataSet ExecuteDataSet(string procedureName,CommandType cmdType);
        IDataReader ExecuteReader(string procedureName, CommandType cmdType);
        void ExecuteNonQuery(string procedureName, CommandType cmdType);
    }
}
