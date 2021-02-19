using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
    public class Menus : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "role_menu_id")]
        public string RoleMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_id")]
        public string MenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_name_txt")]
        public string MenuName { get; set; }

        [DatabaseColumnName(ColumnName = "controller_name_txt")]
        public string ControllerName { get; set; }

        [DatabaseColumnName(ColumnName = "menu_category_id")]
        public string MenuCategoryId { get; set; }

        [DatabaseColumnName(ColumnName = "locked_ind")]
        public bool? LockedInd { get; set; }

        [DatabaseColumnName(ColumnName = "sub_menu_ind")]
        public bool SubMenuInd { get; set; }

        [DatabaseColumnName(ColumnName = "parent_menu_id")]
        public string ParentMenuId { get; set; }

        [DatabaseColumnName(ColumnName = "active_ind")]
        public bool ActiveInd { get; set; }

        private string _created_by;

        [DatabaseColumnName(ColumnName = "created_by")]
        public string CreatedBy
        {
            get { return _created_by; }
            set { _created_by = value; }
        }

        private DateTime _created_dt;

        [DatabaseColumnName(ColumnName = "created_dt")]
        public DateTime CreatedDate
        {
            get { return _created_dt; }
            set { _created_dt = value; }
        }

        private string _updated_by;

        [DatabaseColumnName(ColumnName = "updated_by")]
        public string UpdatedBy
        {
            get { return _updated_by; }
            set { _updated_by = value; }
        }

        private DateTime _updated_dt;

        [DatabaseColumnName(ColumnName = "updated_dt")]
        public DateTime UpdatedDate
        {
            get { return _updated_dt; }
            set { _updated_dt = value; }
        }

    }
}
