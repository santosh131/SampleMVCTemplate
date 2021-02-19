using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
    public class Roles : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        [DatabaseColumnName(ColumnName = "role_name_txt")]
        public string RoleName { get; set; }

        [DatabaseColumnName(ColumnName = "active_ind")]
        public bool ActiveIndicator
        {
            get; set;
        }

        private string _created_by;

        [DatabaseColumnName(ColumnName = "created_by")]
        public string CreatedBy
        {
            get { return _created_by; }
            set { _created_by = value; }
        }

        private Nullable<DateTime> _created_dt;

        [DatabaseColumnName(ColumnName = "created_dt")]
        public Nullable<DateTime> CreatedDate
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

        private Nullable<DateTime> _updated_dt;

        [DatabaseColumnName(ColumnName = "updated_dt")]
        public Nullable<DateTime> UpdatedDate
        {
            get { return _updated_dt; }
            set { _updated_dt = value; }
        }
    }
}
