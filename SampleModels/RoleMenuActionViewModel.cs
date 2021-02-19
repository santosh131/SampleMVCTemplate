using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
    public class RoleMenuActionViewModel : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "role_menu_action_id")]
        public string RoleMenuActionId { get; set; }

        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        [DatabaseColumnName(ColumnName = "name_txt")]
        public string Name { get; set; }

        [DatabaseColumnName(ColumnName = "role_menu_id")]
        public string RoleMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_id")]
        public string MenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_name_txt")]
        public string MenuName { get; set; }

        [DatabaseColumnName(ColumnName = "menu_category_id")]
        public string MenuCategoryId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_ind")]
        public bool MenuIndicator { get; set; }

        [DatabaseColumnName(ColumnName = "controller_name_txt")]
        public string ControllerName { get; set; }

        [DatabaseColumnName(ColumnName = "sub_menu_ind")]
        public bool SubMenuInd { get; set; }

        [DatabaseColumnName(ColumnName = "parent_menu_id")]
        public string ParentMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_id")]
        public string MenuActionId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_cd")]
        public string MenuActionCode { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_name_txt")]
        public string MenuActionName { get; set; }

        [DatabaseColumnName(ColumnName = "action_desc_txt")]
        public string ActionDescription { get; set; }

        [DatabaseColumnName(ColumnName = "selected_ind")]
        public Boolean SelectedIndicator { get; set; }

        [DatabaseColumnName(ColumnName = "edit_locked_ind")]
        public Boolean EditLockedIndicator { get; set; }

    }
}
