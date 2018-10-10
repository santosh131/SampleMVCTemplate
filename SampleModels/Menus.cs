using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    public class Menus
    {
        [DatabaseColumnName(ColumnName = "menu_id")]
        public string MenuId { get; set; }

        [DatabaseColumnName(ColumnName = "name_txt")]
        public string Name { get; set; }

        [DatabaseColumnName(ColumnName = "controller_name_txt")]
        public string ControllerName { get; set; }

        [DatabaseColumnName(ColumnName = "sub_menu_ind")]
        public bool SubMenuInd { get; set; }

        [DatabaseColumnName(ColumnName = "parent_menu_ind")]
        public string ParentMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "active_ind")]
        public bool ActiveInd { get; set; }

        public MenuActions MenuActions
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
