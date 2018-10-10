using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace SampleHelpers
{
    public class CustomSqlConnection : ICustomConnection
    {
       
        #region Properties

        private List<SqlParameter> _parameters;

        public List<SqlParameter> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
        private SqlConnection _conn;

        public SqlConnection Connection
        {
            get { return _conn; }
            set { _conn = value; }
        }

        #endregion

        #region Constructors

        public CustomSqlConnection()
        {
            _parameters = new List<SqlParameter>();
        }

        public CustomSqlConnection(string name)
        {
            _conn = new SqlConnection();
            _conn.ConnectionString = name;
            _parameters = new List<SqlParameter>();
        }

        #endregion

        #region Private Methods

        private void PrepareCommand(ref SqlCommand cmd)
        {
            if (_parameters != null)
            {
                for (int i = 0; i < _parameters.Count; i++)
                {
                    cmd.Parameters.Add(_parameters[i]);
                }
            }
        }

        #endregion

        #region Public Methods

        public void SetConnectionObject(string connString)
        {
            try
            {
                _conn = new SqlConnection();
                _conn.ConnectionString = connString;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddTableParameters(string name, SqlDbType dbType, ParameterDirection paramDirection, int size, object value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;
            param.Size = size;
            param.SqlDbType = dbType;           
            param.Direction = paramDirection;
            if (value != null)
                param.Value = value;
            else
                param.Value = DBNull.Value;

            _parameters.Add(param);
        } 

        public void AddParameters(string name, DbType dbType, ParameterDirection paramDirection, int size, object value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;
            param.Size = size;
            param.DbType = dbType;
            param.Direction = paramDirection;
            if (value != null)
                param.Value = value;
            else
                param.Value = DBNull.Value;

            _parameters.Add(param);
        }

        public DataSet ExecuteDataSet(string procedureName, CommandType cmdType)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand(procedureName, _conn);                
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = cmdType;
                PrepareCommand(ref cmd);
                da.SelectCommand = cmd;
                _conn.Open();
                DataSet ds = new DataSet();
                da.Fill(ds);
                _conn.Close();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        
        public IDataReader ExecuteReader(string procedureName, CommandType cmdType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(procedureName, _conn);
                cmd.CommandType = cmdType;
                PrepareCommand(ref cmd);
                _conn.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ExecuteNonQuery(string procedureName, CommandType cmdType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(procedureName, _conn);
                cmd.CommandType = cmdType;
                PrepareCommand(ref cmd);
                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
