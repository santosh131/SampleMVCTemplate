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
   public class RolesBO
    {
        public List<Roles> GetRoles(Roles roles, out string messageCode, out string message, out int totPages)
        {
            messageCode = string.Empty;
            message = string.Empty;
            return ConversionHelpers.ConvertToList<Roles>(new RolesDAL().GetRoles(roles, out messageCode, out message,out totPages));
        }

        public Roles GetRole(Roles roles, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            List<Roles> uLst = ConversionHelpers.ConvertToList<Roles>(new RolesDAL().GetRole(roles, out messageCode, out message));
            if (uLst.Count > 0)
                return uLst[0];
            return null;
        }

        public List<RoleMenuActionViewModel> GetRoleMenuAction(RoleMenus roleMenus, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            return ConversionHelpers.ConvertToList<RoleMenuActionViewModel>(new RolesDAL().GetRoleMenuAction(roleMenus, out messageCode, out message));            
        }

        public void RolesCUD(Roles roles, out string roleId, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            roleId = string.Empty;
            new RolesDAL().RolesCUD(roles, out roleId, out messageCode, out message);
        }

        public void RoleMenusCUD(RoleMenuViewModel rolesmVM, RoleMenuAction rma, out string messageCode, out string message)
        {
            messageCode = string.Empty;
            message = string.Empty;
            new RolesDAL().RoleMenusCUD(rolesmVM, rma, out messageCode, out message);
        }
    }
}
