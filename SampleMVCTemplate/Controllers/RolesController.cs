using Helpers;
using SampleBO;
using SampleHelpers;
using SampleModels;
using SampleMVCTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Controllers
{
    public class RolesController : BaseController
    {
        // GET: Roles
        public ActionResult Index(string isMenu)
        {
            SessionHelper.ClearSearchSession(isMenu);
            Roles roles = new Roles();
            roles = SessionHelper.GetSearchSession<Roles>("RolesSearch");

            if (string.IsNullOrEmpty(roles.StatusCode))
                roles.StatusCode = "1";//Default active

            return View(roles);
        }

        [HttpGet]
        [ActionName("Search")]
        public JsonResult Search(Roles roles)
        {
            MessageModel mm = new MessageModel();
            if (roles == null)
                roles = new Roles();

            if (string.IsNullOrEmpty(roles.StatusCode))
                roles.StatusCode = "1"; //Default active

            SessionHelper.SetSearchSession("RolesSearch", roles);
            string messageCode = "";
            string message = "";
            int totalPages = 1; 
            List<Roles> lstRoles = new List<Roles>();
            try
            {
                lstRoles = new RolesBO().GetRoles(roles, out messageCode, out message, out totalPages);
                mm.HtmlContent = RenderRazorViewToString("Search", lstRoles);
                mm.MessageCode = messageCode;
                mm.MessageText = message;
                mm.TotalPageCount = totalPages;
            }
            catch (Exception ex)
            {
                mm.HtmlContent = RenderRazorViewToString("Search", lstRoles);
                mm.MessageCode = CommonEnums.MessageCodes.ERROR.ToString();
                mm.MessageText = ex.Message;
                mm.TotalPageCount = 1;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        // GET: Roles/Edit/5

        public ActionResult CreateUpdateDeleteSaveIndex(string RoleId, string CUDOperationType)
        {
            Roles roles = new Roles();
            roles.RoleId = RoleId;
            roles.CUDOperationType = CUDOperationType;

            return View(roles);
        }

        [HttpGet]
        [ActionName("CreateUpdateDeleteSaveView")]
        public JsonResult CreateUpdateDeleteSaveView(string RoleId, string CUDOperationType)
        {
            MessageModel mm = new MessageModel();
            Roles roles = new Roles();
            string messageCode = "";
            string message = "";
            Roles r = new Roles();
            roles.RoleId = RoleId;

            try
            {

                if (CUDOperationType != CommonEnums.CreateOperationType)
                {
                    r = new RolesBO().GetRole(roles, out messageCode, out message);                     
                    if (r == null)
                    {
                        r = new Roles();
                    }
                }
                else
                {
                    messageCode = CommonEnums.MessageCodes.SUCCESS_GET.ToString();
                }
                r.CUDOperationType = CUDOperationType;
                mm.HtmlContent = RenderRazorViewToString("CreateUpdateDeleteSaveView", r);
                mm.MessageCode = messageCode;
                mm.MessageText = message;

            }
            catch (Exception ex)
            {
                mm.HtmlContent = RenderRazorViewToString("CreateUpdateDeleteSaveView", r);
                mm.MessageCode = CommonEnums.MessageCodes.ERROR.ToString();
                mm.MessageText = ex.Message;

            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateUpdateDeleteSaveOperation(Roles roles, FormCollection collection)
        {
            MessageModel mm = new MessageModel();
            try
            {
                string messageCode = "";
                string message = "";
                string roleId = "";

                roles.CreatedBy = SessionHelper.GetUserName();
                roles.UpdatedBy = SessionHelper.GetUserName();

                if (roles.CUDOperationType == CommonEnums.CreateOperationType)
                {
                    roles.ActiveIndicator = true;
                }

                if (roles.CUDOperationType == CommonEnums.ActivateInactivateOperationType)
                {
                    if (roles.ActiveIndicator)
                        roles.ActiveIndicator = false;
                    else
                        roles.ActiveIndicator = true;
                }

                new RolesBO().RolesCUD(roles, out roleId, out messageCode, out message);

                mm.MessageCode = messageCode;
                mm.MessageText = message;

                return Json(mm);
            }
            catch (Exception ex)
            {
                mm.MessageCode = CommonEnums.MessageCodes.ERROR.ToString();
                mm.MessageText = ex.Message;

                return Json(mm);
            }
        }

        // GET: Roles/Menu/5
        public ActionResult Menu(string id)
        {
            ViewBag.Menus = new CommonBO().GetMenusLookup(CommonHelpers.OptionCodeSelect);

            RoleMenuViewModel rmvm = new RoleMenuViewModel();
            rmvm.RoleId = id;
            rmvm.MenuId = "";

            return View(rmvm);
        }

        // GET: Roles/Menu/5
        public ActionResult MenuActions(string RoleId, string MenuId)
        {
            ViewBag.Menus = new CommonBO().GetMenusLookup(CommonHelpers.OptionCodeSelect);
            RoleMenus roleMenu = new RoleMenus();
            string messageCode = "";
            string message = "";

            roleMenu.RoleId = RoleId;
            roleMenu.MenuId = MenuId;

            RoleMenuViewModel rmvm = new RoleMenuViewModel();
            List<RoleMenuActionViewModel> lstrmvm = new RolesBO().GetRoleMenuAction(roleMenu, out messageCode, out message);
            rmvm.RoleId = roleMenu.RoleId;
            rmvm.MenuId = roleMenu.MenuId;
            rmvm.RoleMenuAction = lstrmvm;

            if (messageCode != CommonEnums.MessageCodes.SUCCESS.ToString())
            {
                SetErrorMessage(message);
            }

            return PartialView("MenuActions", rmvm);
        }

        // POST: Roles/Menu/5
        [HttpPost]
        public JsonResult MenuActionsSave(RoleMenuViewModel rolesmVM, FormCollection collection)
        {
            MessageModel mm = new MessageModel();
            try
            {
                string messageCode = "";
                string message = "";
                RoleMenuAction rma = new RoleMenuAction();
                rma.CreatedBy = SessionHelper.GetUserName();

                rma.CUDOperationType = CommonEnums.SaveOperationType;
                DataTable dt = TableTypeHelpers.GetMenuActionTable();
                DataRow dr;
                foreach (var item in rolesmVM.RoleMenuAction)
                {
                    if (item.SelectedIndicator || item.EditLockedIndicator)
                    {
                        dr = dt.NewRow();
                        dr[0] = item.MenuActionId;
                        dr[1] = item.SelectedIndicator;
                        dt.Rows.Add(dr);
                    }
                }
                rolesmVM.RoleMenuActionDt = dt;
                new RolesBO().RoleMenusCUD(rolesmVM, rma, out messageCode, out message);
                mm.MessageCode = messageCode;
                mm.MessageText = message;

                return Json(mm);
            }
            catch (Exception ex)
            {
                mm.MessageCode = CommonEnums.MessageCodes.ERROR.ToString();
                mm.MessageText = ex.Message;

                return Json(mm);
            }
        }
    }
}
