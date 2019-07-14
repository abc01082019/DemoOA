using Spring.Web.Mvc;
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
    public class MvcApplication : SpringMvcApplication //System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 从配置文件中读取log4net的配置, 然后进行一个初始化工作
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
