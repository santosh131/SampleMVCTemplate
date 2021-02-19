using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
  public  class RoleMenuViewModel : ModelHelpers
    {
        [DatabaseColumnName(ColumnName = "role_id")]
        public string RoleId { get; set; }

        [DatabaseColumnName(ColumnName = "menu_id")]
        public string MenuId { get; set; }

        public List<RoleMenuActionViewModel> RoleMenuAction { get; set; }

        public DataTable RoleMenuActionDt { get; set; }
    }
}
