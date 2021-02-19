using Helpers;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL
{
    public class CommonDAL : DataAccessObject
    {
        public IDataReader GetRolesLookup(string optionCode)
        {
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_option_cd", DbType.String, ParameterDirection.Input, 30, optionCode);            
            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_ROLES_LOOKUP", CommandType.StoredProcedure);            
            return rdr;
        }

        public IDataReader GetMenusLookup(string optionCode)
        {
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_option_cd", DbType.String, ParameterDirection.Input, 30, optionCode);
            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_MENUS_LOOKUP", CommandType.StoredProcedure);
            return rdr;
        }

        public IDataReader GetCommonFieldLookup(string fieldCode,string optionCode)
        {
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_field_cd", DbType.String, ParameterDirection.Input, 30, fieldCode);
            CustomConnection.AddParameters("@pin_option_cd", DbType.String, ParameterDirection.Input, 2, optionCode);
            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_COMMON_DDL", CommandType.StoredProcedure);
            return rdr;
        }
    }
}
