using MAP.OA.Common;
using MAP.OA.IBLL;
using MAP.OA.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    [LoginCheckFilter(ActivateCheck = false)]
    public class UserLoginController : Controller
    {
        public IUserInfoService UserInfoService { get; set; }

        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        #region Validation Code
        /// <summary>
        /// Create a validate code/images and return it
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowValidationCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);

            // 验证码放到Session中
            Session["validationCode"] = strCode;


            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);

            return File(imgBytes, @"image/jpeg");
        }
        #endregion

        #region Process login
        /// <summary>
        /// process login info
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessLogin()
        {
            
            // 1.处理验证码
            // 获取表单中的验证码
            #region Check Validation code
            string strCode = Request["LoginValidationCode"];
            // 拿到Session中的验证码
            string sessionCode = Session["validationCode"] as string;
            Session["validationCode"] = null;

            if (string.IsNullOrEmpty(sessionCode) || strCode != sessionCode)
            {
                return Content("Incorrect validation code");
            }
            #endregion

            // 2.处理验证码
            string loginName = Request["LoginName"];
            string loginPwd = Request["LoginPwd"];
            short notDeleted = (short)MAP.OA.Model.Enum.DelFlagEnum.Normal;
            var userInfo = UserInfoService.GetEntities(u => u.UserName == loginName && u.Password == loginPwd && u.DelFlag == notDeleted).FirstOrDefault();

            if (userInfo == null)
            {
                return Content("Incorrect userName or password");
            }

            //Session["loginUser"] = userInfo;

            // Use memcache+cookie(distribute system) instead of Session(Session is slow and only work on one server)
            // Assign immediately a flag: Guid. Store Guid as key in the mm and user-object as value in the mm.

            // Generate a Guid
            string userLoginGuid = Guid.NewGuid().ToString();

            // write Guid to client's Cookie
            Response.Cookies["userLoginGuid"].Value = userLoginGuid;

            // Add client's data to mm. 
            // Need to solve a problem: Store the data as a Single or Distributed system
            // Store user's guid as key and user-object as value in the cache for 20 minuters
            Common.Cache.CacheHelper.AddCache(userLoginGuid, userInfo, DateTime.Now.AddMinutes(20));

            // login successful
            return Content("LoginSuccessful");

        }
        #endregion

    }
}