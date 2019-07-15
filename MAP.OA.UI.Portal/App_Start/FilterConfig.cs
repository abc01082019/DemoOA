using MAP.OA.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());


            //ActionFilter, ResultFilter, ExceptionFilter

            //第三种filter: 异常过滤器
            // Add our own ExceptionFilter
            filters.Add(new MyExceptionFilter());


        }
    }
}