using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Models
{
    public class MyExceptionFilter: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            // 依然让父类先处理
            base.OnException(filterContext);


            // 直接把错误信息写到日志文件离去/Add the exception into a log by using Common.LogHelper
            Common.LogHelper.WriteLog(filterContext.Exception.ToString());
        }
    }
}