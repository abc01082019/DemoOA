using MAP.OA.BLL;
using MAP.OA.IBLL;
using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        IUserInfoService userInfoService = new UserInfoService(); // Spring.Net
        public ActionResult Index()
        {   
            return View(userInfoService.GetEntities(u => true));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
    }
}