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
    public class UserDAL : DataAccessObject
    {
        public IDataReader GetUsers(UsersViewModel usersViewModel, out string messageCode, out string message, out int totPages)
        {
            messageCode = string.Empty;
            message = string.Empty;
            totPages = 1;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_user_name_txt", DbType.String, ParameterDirection.Input, 30, usersViewModel.UserName);
            CustomConnection.AddParameters("@pin_last_name_txt", DbType.String, ParameterDirection.Input, 30, usersViewModel.LastName);
            CustomConnection.AddParameters("@pin_first_name_txt", DbType.String, ParameterDirection.Input, 30, usersViewModel.FirstName);
            CustomConnection.AddParameters("@pin_status_cd", DbType.String, ParameterDirection.Input, 30, usersViewModel.StatusCode);
            CustomConnection.AddParameters("@pin_role_id_txt", DbType.String, ParameterDirection.Input, 50, usersViewModel.RoleId);
            CustomConnection.AddParameters("@pin_page_index", DbType.Int32, ParameterDirection.Input, 0, usersViewModel.PageIndex.ToInteger());
            CustomConnection.AddParameters("@pin_sort_col_name_txt", DbType.String, ParameterDirection.Input, 50, usersViewModel.SortColumnName);
            CustomConnection.AddParameters("@pin_sort_asc_desc", DbType.String, ParameterDirection.Input, 50, usersViewModel.SortAscDesc);
            CustomConnection.AddParameters("@pout_tot_pages", DbType.Int32, ParameterDirection.Output, 0, 0);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_USERS_LIST", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            totPages = CustomConnection.GetParameterValue("@pout_tot_pages").ToInteger();
            return rdr;
        }

        public IDataReader GetLoginUser(string userName, string password, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_user_name_txt", DbType.String, ParameterDirection.Input, 30, userName);
            CustomConnection.AddParameters("@pin_pwd_txt", DbType.String, ParameterDirection.Input, 200, password);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_LOGIN_USER", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            return rdr;

        }

        public IDataReader GetUser(string userId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_user_id_txt", DbType.String, ParameterDirection.Input, 50, userId);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_USER", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            return rdr;
        }


        public IDataReader GetUserRolesMenusActions(string userName, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_user_name_txt", DbType.String, ParameterDirection.Input, 30, userName);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_USER_ROLES_MENUS_LIST", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            return rdr;
        }

        public void UserSessionCUD(UserSession userSession, out string sessionId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            CustomConnection.AddParameters("@pin_cud_operation_type", DbType.String, ParameterDirection.Input, 5, userSession.CUDOperationType);
            CustomConnection.AddParameters("@pin_user_id_txt", DbType.String, ParameterDirection.Input, 50, userSession.UserId);
            CustomConnection.AddParameters("@pin_user_name_txt", DbType.String, ParameterDirection.Input, 30, userSession.UserName);
            CustomConnection.AddParameters("@pin_role_action_menu_txt", DbType.String, ParameterDirection.Input, 0, userSession.RoleMenuAction);
            CustomConnection.AddParameters("@pout_session_id_txt", DbType.String, ParameterDirection.Output, 50, "");
            CustomConnection.AddParameters("@pin_session_id_txt", DbType.String, ParameterDirection.Input, 50, userSession.SessionId);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            CustomConnection.ExecuteNonQuery("SP_USER_SESSION_CUD", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            sessionId = CustomConnection.GetParameterValue("@pout_session_id_txt");

        }


        public IDataReader GetUserSession(UserSession userSession, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader rdr = null;
            CustomConnection.AddParameters("@pin_session_id_txt", DbType.String, ParameterDirection.Input, 50, userSession.SessionId);
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            rdr = CustomConnection.ExecuteDataSetCreateDataReader("SP_GET_USER_SESSION", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            return rdr;
        }

        public void UserCUD(UsersViewModel usersViewModel, out string userId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            CustomConnection.AddParameters("@pin_cud_operation_type", DbType.String, ParameterDirection.Input, 5, usersViewModel.CUDOperationType);
            CustomConnection.AddParameters("@pin_user_id_txt", DbType.String, ParameterDirection.Input, 50, usersViewModel.UserId);
            CustomConnection.AddParameters("@pin_user_name_txt", DbType.String, ParameterDirection.Input, 30, usersViewModel.UserName);
            CustomConnection.AddParameters("@pin_last_name_txt", DbType.String, ParameterDirection.Input, 30, usersViewModel.LastName);
            CustomConnection.AddParameters("@pin_first_name_txt", DbType.String, ParameterDirection.Input, 30, usersViewModel.FirstName);
            CustomConnection.AddParameters("@pin_email_id_txt", DbType.String, ParameterDirection.Input, 100, usersViewModel.Email);
            CustomConnection.AddParameters("@pin_mobile_no_txt", DbType.String, ParameterDirection.Input, 100, usersViewModel.Mobile);
            CustomConnection.AddParameters("@pin_addr1_txt ", DbType.String, ParameterDirection.Input, 100, usersViewModel.Address1);
            CustomConnection.AddParameters("@pin_addr2_txt ", DbType.String, ParameterDirection.Input, 100, usersViewModel.Address2);
            CustomConnection.AddParameters("@pin_state_cd  ", DbType.String, ParameterDirection.Input, 10, usersViewModel.StateCode);
            CustomConnection.AddParameters("@pin_city_txt  ", DbType.String, ParameterDirection.Input, 100, usersViewModel.City);
            CustomConnection.AddParameters("@pin_zip_cd_txt", DbType.String, ParameterDirection.Input, 15, usersViewModel.ZipCode);
            CustomConnection.AddParameters("@pin_pwd_txt   ", DbType.String, ParameterDirection.Input, 200, usersViewModel.Password);
            CustomConnection.AddParameters("@pin_role_id   ", DbType.String, ParameterDirection.Input, 50, usersViewModel.RoleId);
            CustomConnection.AddParameters("@pin_active_ind", DbType.Boolean, ParameterDirection.Input, 0, usersViewModel.ActiveIndicator.ToBoolean(true));
            CustomConnection.AddParameters("@pin_created_by", DbType.String, ParameterDirection.Input, 30, usersViewModel.CreatedBy);
            CustomConnection.AddParameters("@pin_updated_by", DbType.String, ParameterDirection.Input, 30, usersViewModel.UpdatedBy);
            CustomConnection.AddParameters("@pout_user_id_txt", DbType.String, ParameterDirection.Output, 50, "");
            CustomConnection.AddParameters("@pout_msg_cd", DbType.String, ParameterDirection.Output, 30, "");
            CustomConnection.AddParameters("@pout_msg_txt", DbType.String, ParameterDirection.Output, 250, "");

            CustomConnection.ExecuteNonQuery("SP_USER_CUD", CommandType.StoredProcedure);
            messageCode = CustomConnection.GetParameterValue("@pout_msg_cd");
            message = CustomConnection.GetParameterValue("@pout_msg_txt");
            userId = CustomConnection.GetParameterValue("@pout_user_id_txt");

        }
    }
}
