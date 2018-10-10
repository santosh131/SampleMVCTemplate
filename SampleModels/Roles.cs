using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public  class Roles
    {
        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        [DatabaseColumnName(ColumnName = "name_txt")]
        public string Name { get; set; }

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

        public RoleMenus RoleMenus
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
