using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    public class Users: ModelHelpers
    {
        private string _user_id;

        [DatabaseColumnName(ColumnName = "user_id")]
        public string UserId
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        private string  _user_name;

        [DatabaseColumnName(ColumnName = "user_name")]
        public string  UserName
        {
            get { return _user_name; }
            set { _user_name = value; }
        }

        private string _pwd_txt;

        [DatabaseColumnName(ColumnName = "pwd_txt")]
        public string Password
        {
            get { return _pwd_txt; }
            set { _pwd_txt = value; }
        }

        public UserRoles UserRoles
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
