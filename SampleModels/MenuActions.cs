using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public class MenuActions
    {
        [DatabaseColumnName(ColumnName = "menu_action_id")]
        public string MenuActionId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_id")]
        public string MenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_cd")]
        public string MenuActionCode { get; set; }

        [DatabaseColumnName(ColumnName = "action_desc_txt")]
        public string ActionDescription { get; set; }

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
