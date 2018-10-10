using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public class UserRoles
    {
        [DatabaseColumnName(ColumnName = "user_role_id")]
        public string UserRoleId { get; set; }

        [DatabaseColumnName(ColumnName = "user_id")]
        public string UserId { get; set; }

        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        public UserRoleMenuAction UserRoleMenuAction
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
