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
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
                else
                {
                    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo;
                }
            }
        }
    }
}