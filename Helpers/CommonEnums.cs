using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class CommonEnums
    {
        public enum DataBases
        {
            Sql=1,
            Oracle=2
        }

        public enum MessageCodes
        {
            Success = 1,
            Error = 2
        }
    }
}
