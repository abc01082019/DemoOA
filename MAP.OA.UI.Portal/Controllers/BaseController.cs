using MAP.OA.Model;
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