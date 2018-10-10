using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    public class UserRoleMenuAction
    {
        [DatabaseColumnName(ColumnName = "user_role_menu_action_id")]
        public string UserRoleMenuActionId { get; set; }

        [DatabaseColumnName(ColumnName = "user_role_id")]
        public string UserRoleId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_id")]
        public string MenuActionId { get; set; }
    }
}
