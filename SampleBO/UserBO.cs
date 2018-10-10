using Helpers;
using SampleDAL;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBO
{
    public class UserBO
    {
        public Users GetUser(string user, string password, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            List<Users> uLst = ConversionHelpers.ConvertToList<Users>(new UserDAL().GetUser(user, password, out messageCode,out message));
            if (uLst.Count > 0)
                return uLst[0];
            return null;
        }
    }
}
