using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        IUserInfoService UserInfoService { get; set; }
        IActionInfoService ActionInfoService { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Menus = LoadUsers();
            return View(this.LoginUser);
        }

        public List<ActionInfo> LoadUsers()
        {
            int userId = this.LoginUser.Id;
            // search and get the relational values
            var user = UserInfoService.GetEntities(u => u.Id == userId).FirstOrDefault();
            // get all the role-actions for the current user
            var allUserRole = user.RoleInfo;
            var allRoleActionIds = (from r in allUserRole
                                from a in r.ActionInfo
                                select a.Id).ToList();
            // get all the deny actions for the user
            var allDenyActionsIds = (from r in user.R_UserInfo_ActionInfo
                                 where r.HasPermission == false
                                 select r.ActionInfoId).ToList();
            // get all the role-actions(without the deny actions) for the user
            var allCRoleActionIds = (from a in allRoleActionIds
                                     where !allDenyActionsIds.Contains(a)
                                     select a).ToList();

            // get all the special confirm actions for the user
            var allActionIds = (from t in user.R_UserInfo_ActionInfo
                                   where t.HasPermission == true
                                   select t.ActionInfoId).ToList();

            // merge all the speacial action with all the user.role.action for the current user
            allActionIds.AddRange(allCRoleActionIds.AsEnumerable());
            allActionIds = allActionIds.Distinct().ToList();

            var actionList = ActionInfoService.GetEntities(a => allActionIds.Contains(a.Id) && a.IsMenu == true && a.DelFlag == (short)DelFlagEnum.Normal).ToList();
            return actionList;
            //{ icon: '/Content/ligerui/Source/lib/images/icons/documents document edit.png', title: 'UserInfo', url: '/UserInfo/Index' }
            //var data = from a in actionList
            //           select new { icon = a.MenuIcon, title = a.ActionName, url = a.Url };
            //return Json(data, JsonRequestBehavior.AllowGet);

        }
    }

}