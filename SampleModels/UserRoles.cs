using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
    public class UserRoles : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "user_role_id")]
        public string UserRoleId { get; set; }

        [DatabaseColumnName(ColumnName = "user_id")]
        public string UserId { get; set; }

        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

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
    }
}
