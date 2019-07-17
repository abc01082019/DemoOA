using MAP.OA.BLL;
using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.UI.Portal.Models;
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
        //IUserInfoService UserInfoService = new UserInfoService(); // Spring.Net
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            //throw new Exception("This is from UserInfoController..........................");
            return View(UserInfoService.GetEntities(u => true));
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
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
    }
}