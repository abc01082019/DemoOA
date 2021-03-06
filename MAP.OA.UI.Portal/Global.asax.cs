﻿using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Spring.Context.Support;
using MAP.OA.UI.Portal.App_Start;

namespace MAP.OA.UI.Portal
{
    public class MvcApplication : Spring.Web.Mvc.SpringMvcApplication //System.Web.HttpApplication
    {
        // Program will execute SpringMvcApplication.Application_Start(object sender, EventArgs e) method as default, 
        //      we should override the Application_Start(object sender, EventArgs e) to customize our own method
        protected override void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // 从配置文件中读取log4net的配置, 然后进行一个初始化工作
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
