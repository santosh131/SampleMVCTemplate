using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class CommonEnums
    {
        public const string DataSeperator = "|+|";
        public const string ListRecordSeperatorSplitter = "|&|";
        public const string PropertySeperator = "|-|";
        public const string PropertyValueSeperator = "|:|";
        public const string SessionKeyHelper = "SessionSearch";
        public const string CreateOperationType = "A";
        public const string UpdateOperationType = "E";
        public const string DeleteOperationType = "D";
        public const string ActivateInactivateOperationType = "AI";
        public const string SaveOperationType = "S";

        public enum DataBases
        {
            Sql = 1,
            Oracle = 2
        }

        public enum MessageCodes
        {
            SUCCESS = 1,
            ERROR = 2,
            SUCCESS_LIST = 3,
            SUCCESS_GET = 4
        }


    }
}
