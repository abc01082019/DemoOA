﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Models
{
    public class LoginCheckFilterAttribute : ActionFilterAttribute
    {
        public bool ActivateCheck { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (ActivateCheck)
            {
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
            }
        }
    }
}