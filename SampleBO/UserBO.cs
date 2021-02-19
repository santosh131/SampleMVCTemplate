using Helpers;
using SampleDAL;
using SampleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SampleBO
{
    public class UserBO
    {
        public List<UsersViewModel> GetUsers(UsersViewModel usersViewModel, out string messageCode, out string message, out int totPages)
        {
            messageCode = string.Empty;
            message = string.Empty;
            return  ConversionHelpers.ConvertToList<UsersViewModel>(new UserDAL().GetUsers(usersViewModel, out messageCode, out message,out totPages));            
        }

        public Users GetLoginUser(string userName, string password, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            List<Users> uLst = ConversionHelpers.ConvertToList<Users>(new UserDAL().GetLoginUser(userName, password, out messageCode, out message));
            if (uLst.Count > 0)
                return uLst[0];
            return null;
        }

        public UsersViewModel GetUser(string userId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            List<UsersViewModel> uLst = ConversionHelpers.ConvertToList<UsersViewModel>(new UserDAL().GetUser(userId, out messageCode, out message));
            if (uLst.Count > 0)
                return uLst[0];
            return null;
        }

        public void GetUserRolesMenusActions(string userName, out List<Menus> menus, out List<MenuCategory> menuCatgs, out List<RoleMenuActionViewModel> roleMenuActioVMs, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            IDataReader dr = new UserDAL().GetUserRolesMenusActions(userName, out messageCode, out message);
            menus = ConversionHelpers.ConvertToList<Menus>(dr, false);
            menuCatgs = new List<MenuCategory>();
            if (dr.NextResult())
                menuCatgs = ConversionHelpers.ConvertToList<MenuCategory>(dr, false);

            roleMenuActioVMs = new List<RoleMenuActionViewModel>();
            if (dr.NextResult())
                roleMenuActioVMs = ConversionHelpers.ConvertToList<RoleMenuActionViewModel>(dr);
        }

        public void UserSessionCUD(UserSession userSession, out string sessionId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            sessionId = string.Empty;
            new UserDAL().UserSessionCUD(userSession, out sessionId, out messageCode, out message);
        } 

        public void GetUserSession(UserSession userSession, out UserSession userSessionOutput, out List<Menus> menus, out List<MenuCategory> menusCatgs, out List<RoleMenuActionViewModel> roleMenuActioVMs, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            menus = null;
            roleMenuActioVMs = null;
            userSessionOutput = null;
            menusCatgs = null;
            IDataReader dr = new UserDAL().GetUserSession(userSession, out messageCode, out message);
            List<UserSession> usLst = ConversionHelpers.ConvertToList<UserSession>(dr);
            if (usLst != null && usLst.Count > 0)
            {
                userSessionOutput = usLst[0];
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string menusActions = userSessionOutput.RoleMenuAction;
                string[] menusActionsSplit = menusActions.Split(new string[] { CommonEnums.DataSeperator }, StringSplitOptions.None);
                menus = jss.Deserialize<List<Menus>>(menusActionsSplit[0].ToString());
                menusCatgs = jss.Deserialize<List<MenuCategory>>(menusActionsSplit[1]);
                roleMenuActioVMs = jss.Deserialize<List<RoleMenuActionViewModel>>(menusActionsSplit[2]); ;
            }
        }

        public void UserCUD(UsersViewModel usersViewModel, out string userId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            userId = string.Empty;
            new UserDAL().UserCUD(usersViewModel, out userId, out messageCode, out message);
        }

    }
}

