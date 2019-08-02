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
    public class ActionInfoController : BaseController
    {
        public IActionInfoService ActionInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllActionInfos()
        {
            // jQuery easyui: table format: {total:32, rows:[{}{}]}

            // those data are passing through the Request from client, when the table is initialized.
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int totalData = 0;

            string srchName = Request["srchName"];
            string srchRemark = Request["srchRemark"];
    
            #region Old Pagination
            //// Get the current page data
            //// 如果有导航属性时，使用微软的默认序列化方式的时候会出现问题: 循环引用的问题
            //// 解决方案: 直接选要显示的属性(非导航属性)
            //var pageData = ActionInfoService.GetPageEntities(
            //    pageSize,
            //    pageIndex,
            //    out totalData,
            //    u => u.DelFlag == DelFlagEnum.Normal,
            //    u => u.Id,
            //    true).Select(u => new { u.Id, u.ActionName, u.Url, u.HttpMethod, u.IsMenu, u.MenuIcon, u.Sort, u.SubTime, u.ModifiedOn, u.Remark });
            #endregion

            var actionQueryParam = new ActionQueryParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                SrchName = srchName,
                SrchRemark = srchRemark
            };

            var pageData = ActionInfoService.LoadPageData(actionQueryParam)
                .Select(u => new { u.Id, u.ActionName, u.Url, u.HttpMethod, u.IsMenu, u.MenuIcon, u.Sort, u.SubTime, u.ModifiedOn, u.Remark });


            var data = new { total = actionQueryParam.Total, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(ActionInfo actionInfo)
        {
            if (actionInfo.IsMenu == false)
            {
                actionInfo.MenuIcon = null;
            }
            string Url = actionInfo.Url.ToLower();
            string HttpMethod = actionInfo.HttpMethod.ToLower();
            var tmp = ActionInfoService.GetEntities(u => u.Url.ToLower() == Url && u.HttpMethod.ToLower() == HttpMethod);
            if (tmp != null)
                return Content("action already exists: <br/> URL: " + Url + "<br/> HttpMethod: " + HttpMethod);

            actionInfo.ModifiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.DelFlag = (short)DelFlagEnum.Normal;
            ActionInfoService.Add(actionInfo);
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
            ActionInfoService.DeleteListLogical(idList);
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            ActionInfo actionInfo = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = actionInfo;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            actionInfo.ModifiedOn = DateTime.Now;
            if (actionInfo.IsMenu == false)
            {
                actionInfo.MenuIcon = null;
            }
            ActionInfoService.Update(actionInfo);
            return Content("ok");
        }
        #endregion

        #region Upload image
        public ActionResult UploadImage()
        {
            var file = Request.Files["fileMenuIcon"];

            string path = "/UploadFiles/UploadImgs/" + Guid.NewGuid().ToString() + "-" + file.FileName;

            file.SaveAs(Request.MapPath(path));

            return Content(path);
        }
        #endregion

        #region Set role
        public ActionResult SetRole(int id)
        {
            ActionInfo action = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewBag.Action = action;
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal).ToList();
            ViewBag.ActionRoleIds = (from r in action.RoleInfo select r.Id).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SetRoleHandler(ActionInfo actionInfo)
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

            ActionInfoService.SetRole(actionInfo.Id, roleIDs);

            return Content("ok");
        } 
        #endregion
    }
}