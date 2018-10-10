using SampleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL
{
    public class UserDAL :DataAccessObject
    {
        public void Func1()
        {
            
        }

        public IDataReader GetUser(string user,string password,out string messageCode,out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            return rdr;
        }
    }
}
