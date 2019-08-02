using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using MAP.OA.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class RoleInfoController : BaseController
    {
        public IRoleInfoService RoleInfoService { get; set; }

        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllRoleInfos()
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
            //var pageData = ActionInfoService.GetPageEntities(
            //    pageSize,
            //    pageIndex,
            //    out totalData,
            //    u => u.DelFlag == Normal,
            //    u => u.Id,
            //    true).Select(u => new { u.Id, u.ActionName, u.Url, u.HttpMethod, u.IsMenu, u.MenuIcon, u.Sort, u.SubTime, u.ModifiedOn, u.Remark });
            #endregion

            var userQueryParam = new UserQueryParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                SrchName = srchName,
                SrchRemark = srchRemark
            };

            var pageData = RoleInfoService.LoadPageData(userQueryParam)
                .Select(u => new { u.Id, u.RoleName, u.ModifiedOn, u.SubTime, u.Remark });


            var data = new { total = userQueryParam.Total, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = (short)DelFlagEnum.Normal;
            RoleInfoService.Add(roleInfo);
            return Content("ok");
        }

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
            RoleInfoService.DeleteListLogical(idList);
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            RoleInfo roleInfo = RoleInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = roleInfo;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            RoleInfoService.Update(roleInfo);
            return Content("ok");
        }
        #endregion

        public ActionResult Show()
        {
  
            return View(RoleInfoService.GetEntities(u => true).ToList());
        }
    }
}