using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
   public class UsersViewModel:Users
    {
        private string _role_id=string.Empty;

        [DatabaseColumnName(ColumnName = "role_id")]
        [Required]
        public string RoleId
        {
            get { return _role_id; }
            set { _role_id = value; }
        }

        [DatabaseColumnName(ColumnName = "role_name_txt")]
        public string RoleName { get; set; }
    }
}
