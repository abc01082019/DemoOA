using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class WFTempController : BaseController
    {
        private IWF_TempService WF_TempService { get; set; }
        // GET: WFTemp
        public ActionResult Index()
        {
            return View();
        }

        #region Get all info
        public ActionResult GetAllWFTemps()
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
            var pageData = WF_TempService.GetPageEntities(
                pageSize,
                pageIndex,
                out totalData,
                u => u.DelFlag == Normal,
                u => u.Id,
                true).Select(u => new { u.Id, u.TempName, u.Decription, u.TempForm, u.Remark, u.SubTime, u.ActivityType, u.DelFlag });
            #endregion

            var data = new { total = totalData, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(WF_Temp wfTemp)
        {
            wfTemp.DelFlag = (short)DelFlagEnum.Normal;
            wfTemp.SubTime = DateTime.Now;
            WF_TempService.Add(wfTemp);
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
            WF_TempService.DeleteListLogical(idList);
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            WF_Temp wfTemp = WF_TempService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = wfTemp;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(WF_Temp wfTemp)
        {
            WF_TempService.Update(wfTemp);
            return Content("ok");
        }
        #endregion

        #region Show all workflow template
        public ActionResult StartWF()
        {
            return View(WF_TempService.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal));
        } 
        #endregion
    }
}