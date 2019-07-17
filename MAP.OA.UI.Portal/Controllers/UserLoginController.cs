using MAP.OA.Common;
using MAP.OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
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

            Session["loginUser"] = userInfo;

            // login successful
            return Content("OK");

        }
        #endregion

    }
}