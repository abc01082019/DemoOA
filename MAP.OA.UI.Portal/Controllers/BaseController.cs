using MAP.OA.BLL;
using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        protected bool ActivateCheck = true;
        protected UserInfo LoginUser { get; set; }
        /// <summary>
        /// Area of Influence: internal of the inherited controller
        /// This method will run before other methods
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (ActivateCheck)
            {

                #region Store Login GUID in Cache
                // use memcache-Cookie instead of session
                if (Request.Cookies["userLoginGuid"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
                string userGuid = Request.Cookies["userLoginGuid"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;

                if (userInfo == null)
                {
                    // The cache data is expired/overtime, please login again
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
                LoginUser = userInfo;
                // Extend the cache time for 20 minutes
                Common.Cache.CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20)); 
                #endregion

                #region Permission check
                if (LoginUser.UserName == "a")
                {
                    return;
                }

                string currentUrl = Request.Url.AbsolutePath.ToLower();
                string currentHttpMethod = Request.HttpMethod.ToLower();

                IApplicationContext ctx = ContextRegistry.GetContext();
                IActionInfoService ActionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService = ctx.GetObject("R_UserInfo_ActionInfoService") as R_UserInfo_ActionInfoService;
                IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;

                // 1 check if the user has a special permission to the url with the httpmethod
                // check if the Url and the HttpMethod exist
                var actionInfo = ActionInfoService.GetEntities(a => a.Url.ToLower() == currentUrl && a.HttpMethod.ToLower() == currentHttpMethod).FirstOrDefault();
                if (actionInfo == null)
                {
                    // Url or HttpMethod not exist
                    Response.Redirect("/Error.html");
                }

                // check if the current user has permission to the page with the httpmethod
                //var rUAInfo = R_UserInfo_ActionInfoService.GetEntities(u => u.UserInfoId == LoginUser.Id && u.ActionInfoId == actionInfo.Id && u.DelFlag == (short)DelFlagEnum.Normal).FirstOrDefault();
                var rUAInfo = R_UserInfo_ActionInfoService.GetEntities(u => u.UserInfoId == LoginUser.Id && u.DelFlag == (short)DelFlagEnum.Normal).ToList();

                var item = (from r in rUAInfo
                            where r.ActionInfoId == actionInfo.Id
                            select r).FirstOrDefault();
                if (item != null)
                {
                    if (item.HasPermission == true)
                        return;
                    else
                        Response.Redirect("/Error.html");
                }


                // 2
                var user = UserInfoService.GetEntities(u => u.Id == LoginUser.Id && u.DelFlag == (short)DelFlagEnum.Normal).FirstOrDefault();

                // get all user roles
                var allRoles = from r in user.RoleInfo select r;
                // get all role actions
                var actions = from r in allRoles
                              from a in r.ActionInfo
                              select a;
                // check is there exists a role action that match the current action
                var result = (from a in actions
                              where a.Id == actionInfo.Id
                              select a).Count();
                if (result <= 0)
                {
                    Response.Redirect("/Error.html");
                } 
                #endregion

                #region Use Session for login check
                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo;
                //}
                #endregion
            }
        }
    }
}