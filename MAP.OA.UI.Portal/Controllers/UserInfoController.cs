using MAP.OA.BLL;
using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using MAP.OA.Model.Param;
using MAP.OA.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        //IUserInfoService UserInfoService = new UserInfoService(); // Spring.Net
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }


        // GET: UserInfo
        public ActionResult Index()
        {
            //throw new Exception("This is from UserInfoController..........................");
            UserInfo LoginUser = new UserInfo { UserName = "XXX" };
            ViewBag.Model = LoginUser;
            return View();
        }

        #region Create(useless)

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
        #endregion

        #region Get Users
        public ActionResult GetAllUserInfos()
        {
            // jQuery easyui: table format: {total:32, rows:[{}{}]}

            // those data are passing through the Request from client, when the table is initialized.
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int totalData = 0;

            string srchName = Request["srchName"];
            string srchRemark = Request["srchRemark"];


            short Normal = (short)MAP.OA.Model.Enum.DelFlagEnum.Normal;

            #region Old Pagination
            //// Get the current page data
            //// 如果有导航属性时，使用微软的默认序列化方式的时候会出现问题: 循环引用的问题
            //// 解决方案: 直接选要显示的属性(非导航属性)
            //var pageData = UserInfoService.GetPageEntities(
            //    pageSize,
            //    pageIndex,
            //    out totalData,
            //    u => u.DelFlag == Normal,
            //    u => u.Id,
            //    true).Select(u => new { u.Id, u.UserName, u.Password, u.FirstName, u.LastName, u.Remark, u.SubTime, u.ModifiedOn }); 
            #endregion

            var userQueryParam = new UserQueryParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                SrchName = srchName,
                SrchRemark = srchRemark
            };

            var pageData = UserInfoService.LoadPageData(userQueryParam)
                .Select(u => new { u.Id, u.UserName, u.Password, u.FirstName, u.LastName, u.Remark, u.SubTime, u.ModifiedOn });

            var data = new { total = userQueryParam.Total, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Add
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            userInfo.DelFlag = (short)DelFlagEnum.Normal;
            UserInfoService.Add(userInfo);
            return Content("ok");
        }
        #endregion

        #region Delete
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("please select at least one row");
            }

            //soft delete from database
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));
            }
            UserInfoService.DeleteListLogical(idList);
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userinfo)
        {
            UserInfoService.Update(userinfo);
            return Content("ok");
        }
        #endregion

        #region Set Role
        public ActionResult SetRole(int Id)
        {
            UserInfo user = UserInfoService.GetEntities(u => u.Id == Id).FirstOrDefault();
            ViewBag.User = user;
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal).ToList();
            ViewBag.UserRoleIds = (from r in user.RoleInfo select r.Id).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SetRoleHandler(UserInfo userInfo)
        {
            // get the current user and set all the selected roles
            List<int> roleIDs = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    roleIDs.Add(roleId);
                }
            }

            UserInfoService.SetRole(userInfo.Id, roleIDs);

            return Content("ok");
        }
        #endregion

        #region Set Action
        public ActionResult SetAction(int Id)
        {
            UserInfo user = UserInfoService.GetEntities(u => u.Id == Id && u.DelFlag == (short)DelFlagEnum.Normal).FirstOrDefault();
            ViewBag.User = user;

            var RUA = R_UserInfo_ActionInfoService.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal);
            var actionInfo = ActionInfoService.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal);
            return View(actionInfo);
        }

        [HttpPost]
        public ActionResult SetAction(UserInfo userInfo)
        {
            // get the current user and set all the selected roles
            List<int> actionIDs = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("rb_"))
                {
                    int roleId = int.Parse(key.Replace("rb_", ""));
                    actionIDs.Add(roleId);
                }
            }

            //TODO : fix to set special Action
            //UserInfoService.SetAction(userInfo.Id, actionIDs);

            return Content("ok");
        }
        #endregion

        public ActionResult DeleteUserAction(int uId, int aId)
        {
            R_UserInfo_ActionInfo RUA = R_UserInfo_ActionInfoService.GetEntities(
                u => u.UserInfoId == uId && u.ActionInfoId == aId && u.DelFlag == (short)DelFlagEnum.Normal
                ).FirstOrDefault();
            if (RUA != null)
            {
                //RUA.DelFlag = (short)DelFlagEnum.Normal;
                R_UserInfo_ActionInfoService.DeleteLogical(RUA.Id);
            }
            return Content("ok");

        }

        public ActionResult EditUserAction(int uId, int aId, int value)
        {

            R_UserInfo_ActionInfo rUA = R_UserInfo_ActionInfoService.GetEntities(
                u => u.UserInfoId == uId && u.ActionInfoId == aId && u.DelFlag == (short)DelFlagEnum.Normal).FirstOrDefault();

            if (rUA != null)
            {
                rUA.HasPermission = value == 1 ? true : false;
                R_UserInfo_ActionInfoService.Update(rUA);
            }
            else
            {
                R_UserInfo_ActionInfoService.Add(new R_UserInfo_ActionInfo()
                {
                    UserInfoId = uId,
                    ActionInfoId = aId,
                    HasPermission = value == 1 ? true : false,
                    DelFlag = (short)DelFlagEnum.Normal
                });
            }
            return Content("ok");
        }
    }
}