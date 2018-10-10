using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public class RoleMenuAction
    {
        [DatabaseColumnName(ColumnName = "role_menu_action_id")]
        public string RoleMenuActionId { get; set; }

        [DatabaseColumnName(ColumnName = "role_menu_id")]
        public string RoleMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_id")]
        public string MenuActionId { get; set; }
    }
}
