using Helpers;
using SampleBO;
using SampleHelpers;
using SampleModels;
using SampleMVCTemplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCTemplate.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        public ActionResult Index(string isMenu)
        {
            ViewBag.Roles = new CommonBO().GetRolesLookup(CommonHelpers.OptionCodeAll);
            SessionHelper.ClearSearchSession(isMenu);
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel = SessionHelper.GetSearchSession<UsersViewModel>("UserSearch");
            if (string.IsNullOrEmpty(usersViewModel.RoleId))
                usersViewModel.RoleId = CommonHelpers.OptionCodeAll;
            if (string.IsNullOrEmpty(usersViewModel.StatusCode))
                usersViewModel.StatusCode = "1";//Default active
            return View(usersViewModel);
        }

        //[HttpGet]
        [ActionName("Search")]
        public JsonResult Search(UsersViewModel usersViewModel)
        {
            MessageModel mm = new MessageModel();
            if (usersViewModel == null)
            {
                usersViewModel = new UsersViewModel();
                if (string.IsNullOrEmpty(usersViewModel.RoleId))
                    usersViewModel.RoleId = CommonHelpers.OptionCodeAll;
                if (string.IsNullOrEmpty(usersViewModel.StatusCode))
                    usersViewModel.StatusCode = "1";//Default active
            }

            SessionHelper.SetSearchSession("UserSearch", usersViewModel);
            string messageCode = "";
            string message = "";
            int totalPages = 1;
            List<UsersViewModel> users = new List<UsersViewModel>();
            try
            {
                users = new UserBO().GetUsers(usersViewModel, out messageCode, out message, out totalPages);
                mm.HtmlContent = RenderRazorViewToString("Search", users);
                mm.MessageCode = messageCode;
                mm.MessageText = message;
                mm.TotalPageCount = totalPages;
            }
            catch (Exception ex)
            {
                mm.HtmlContent = RenderRazorViewToString("Search", users);
                mm.MessageCode = CommonEnums.MessageCodes.ERROR.ToString();
                mm.MessageText = ex.Message;
                mm.TotalPageCount = 1;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateDeleteSaveIndex(string UserId, string CUDOperationType)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.UserId = UserId;
            usersViewModel.CUDOperationType = CUDOperationType;           
            return View(usersViewModel);
        }

        [HttpGet]
        [ActionName("CreateUpdateDeleteSaveView")]
        public JsonResult CreateUpdateDeleteSaveView(string UserId, string CUDOperationType)
        {
            MessageModel mm = new MessageModel();
            UsersViewModel usersVM = new UsersViewModel();
            string messageCode = "";
            string message = "";
            UsersViewModel r = new UsersViewModel();
            usersVM.UserId = UserId;
            ViewBag.Roles = new CommonBO().GetRolesLookup(CommonHelpers.OptionCodeSelect);
            try
            {

                if (CUDOperationType != CommonEnums.CreateOperationType)
                {
                    r = new UserBO().GetUser(UserId, out messageCode, out message);
                    if (r == null)
                    {
                        r = new UsersViewModel();
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

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateUpdateDeleteSaveOperation(UsersViewModel usersVM, FormCollection collection)
        {
            MessageModel mm = new MessageModel();
            try
            {
                string messageCode = "";
                string message = "";
                string userId = "";

                usersVM.CreatedBy = SessionHelper.GetUserName();
                usersVM.UpdatedBy = SessionHelper.GetUserName();

                if (usersVM.CUDOperationType == CommonEnums.CreateOperationType)
                {
                    usersVM.ActiveIndicator = true;
                }

                if (usersVM.CUDOperationType == CommonEnums.ActivateInactivateOperationType)
                {
                    if (usersVM.ActiveIndicator)
                        usersVM.ActiveIndicator = false;
                    else
                        usersVM.ActiveIndicator = true;
                }

                new UserBO().UserCUD(usersVM, out userId, out messageCode, out message);

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
