using Helpers;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL
{
   public class RolesDAL : DataAccessObject
    {
        public IDataReader GetRoles(Roles roles, out string messageCode, out string message, out int totPages)
        {
            messageCode = string.Empty;
            message = string.Empty;
            totPages = 1;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_role_name_txt", DbType.String, ParameterDirection.Input, 30, roles.RoleName);
            CustomConnection.AddParameters("@pin_status_cd", DbType.String, ParameterDirection.Input, 30, roles.StatusCode);
            CustomConnection.AddParameters("@pin_page_index", DbType.Int32, ParameterDirection.Input, 0, roles.PageIndex.ToInteger());
            CustomConnection.AddParameters("@pin_sort_col_name_txt", DbType.String, ParameterDirection.Input, 50, roles.SortColumnName);
            CustomConnection.AddParameters("@pin_sort_asc_desc", DbType.String, ParameterDirection.Input, 50, roles.SortAscDesc);
            CustomConnection.AddParameters("@pout_tot_pages", DbType.Int32, ParameterDirection.Output, 0, 0);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_ROLES_LIST", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            totPages = CustomConnection.GetParameterValue("@pout_tot_pages").ToInteger();
            return rdr;
        }

        public IDataReader GetRole(Roles roles, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_role_id", DbType.String, ParameterDirection.Input, 50, roles.RoleId);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_ROLE", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            return rdr;
        }

        public IDataReader GetRoleMenuAction(RoleMenus roleMenus, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_role_id", DbType.String, ParameterDirection.Input, 50, roleMenus.RoleId);
            CustomConnection.AddParameters("@pin_menu_id", DbType.String, ParameterDirection.Input, 50, roleMenus.MenuId);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_ROLE_MENU_ACTION", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            return rdr;
        }

        public void RolesCUD(Roles roles, out string roleId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            CustomConnection.AddParameters("@pin_cud_operation_type", DbType.String, ParameterDirection.Input, 5, roles.CUDOperationType);
            CustomConnection.AddParameters("@pin_role_id", DbType.String, ParameterDirection.Input, 50, roles.RoleId);
            CustomConnection.AddParameters("@pin_role_name_txt", DbType.String, ParameterDirection.Input, 30, roles.RoleName);
            CustomConnection.AddParameters("@pin_active_ind", DbType.Boolean, ParameterDirection.Input, 0, roles.ActiveIndicator.ToBoolean(true));
            CustomConnection.AddParameters("@pin_created_by", DbType.String, ParameterDirection.Input, 30, roles.CreatedBy);
            CustomConnection.AddParameters("@pin_updated_by", DbType.String, ParameterDirection.Input, 30, roles.UpdatedBy);
            CustomConnection.AddParameters("@pout_role_id", DbType.String, ParameterDirection.Output, 50, "");
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            CustomConnection.ExecuteNonQuery("SP_ROLE_CUD", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            roleId = CustomConnection.GetParameterValue("@pout_role_id");

        }

        public void RoleMenusCUD(RoleMenuViewModel rolesmVM, RoleMenuAction rma, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            CustomConnection.AddParameters("@pin_cud_operation_type", DbType.String, ParameterDirection.Input, 5, rma.CUDOperationType);
            CustomConnection.AddParameters("@pin_role_id", DbType.String, ParameterDirection.Input, 50, rolesmVM.RoleId);
            CustomConnection.AddParameters("@pin_menu_id", DbType.String, ParameterDirection.Input, 30, rolesmVM.MenuId);
            CustomConnection.AddTableParameters("@pin_role_menu_ut", SqlDbType.Structured, ParameterDirection.Input, 0, rolesmVM.RoleMenuActionDt);
            CustomConnection.AddParameters("@pin_created_by", DbType.String, ParameterDirection.Input, 30, rma.CreatedBy);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            CustomConnection.ExecuteNonQuery("SP_ROLE_MENU_CUD", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
        }
    }
}
