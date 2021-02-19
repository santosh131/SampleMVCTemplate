using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
    public class UserSession: ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "session_id_txt")]
        public string SessionId { get; set; }

        [DatabaseColumnName(ColumnName = "user_id_txt")]
        public string UserId { get; set; }

        [DatabaseColumnName(ColumnName = "user_name_txt")]
        public string UserName { get; set; }

        [DatabaseColumnName(ColumnName = "menu_txt")]
        public string MenuText { get; set; }

        [DatabaseColumnName(ColumnName = "menu_category_txt")]
        public string MenuCategoryText{ get; set; }

        [DatabaseColumnName(ColumnName = "role_menu_action_txt")]
        public string RoleMenuAction { get; set; }
    }
}
