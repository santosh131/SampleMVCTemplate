using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
   public class MenuActions : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "menu_action_id")]
        public string MenuActionId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_id")]
        public string MenuId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_cd")]
        public string MenuActionCode { get; set; }

        [DatabaseColumnName(ColumnName = "menu_action_name_txt")]
        public string MenuActionName { get; set; }

        [DatabaseColumnName(ColumnName = "action_desc_txt")]
        public string ActionDescription { get; set; }

        [DatabaseColumnName(ColumnName = "menu_ind")]
        public bool MenuIndicator { get; set; }

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
