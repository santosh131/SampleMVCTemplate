using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public class RoleMenus
    {
        [DatabaseColumnName(ColumnName = "role_menu_id")]
        public string RoleMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        [DatabaseColumnName(ColumnName = "menuj_id")]
        public string MenuId { get; set; }

        public RoleMenuAction RoleMenuAction
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
