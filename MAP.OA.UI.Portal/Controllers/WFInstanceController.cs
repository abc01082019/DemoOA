using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using MAP.OA.Workflow;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.OA.UI.Portal.Controllers
{
    public class WFInstanceController : Controller
    {
        private IWF_TempService WF_TempService { get; set; }
        private IUserInfoService UserInfoService { get; set; }
        private IWF_InstanceService WF_InstanceService { get; set; }
        private IWF_ProcedureService WF_ProcedureService { get; set; }
        // GET: WFInstance
        public ActionResult Index()
        {
            return View();
        }

        #region Add
        public ActionResult Add(int id)
        {
            // Single data or error
            WF_Temp wfTemp = WF_TempService.GetEntities(u => u.Id == id).SingleOrDefault();
            ViewBag.Temp = wfTemp;

            var users = UserInfoService.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal);
            ViewData["flowTo"] = (from u in users
                                    select new SelectListItem()
                                    {
                                        Selected = false,
                                        Text = u.UserName,
                                        Value = u.Id + ""
                                    }).ToList();
            return View();
        }

        /// <summary>
        /// Create a new workflow instance and create two procedure object(the current procedure and the next flow procedure)
        /// </summary>
        /// <param name="wfInstance">Create a new workflow instance</param>
        /// <param name="id">Workflow template id</param>
        /// <param name="flowTo">The next procedure handler name</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(WF_Instance wfInstance, int id, int flowTo)
        {
            var currentUserId = 1;

            // 1. Add the new data into workflow-instance
            wfInstance.DelFlag = (short)DelFlagEnum.Normal;
            wfInstance.StartTime = DateTime.Now;
            wfInstance.FilePath = string.Empty;
            wfInstance.SenderId = currentUserId;
            wfInstance.Level = 0;
            wfInstance.Status = (short)WF_InstanceEnum.Processing;
            wfInstance.WFInstanceId = Guid.Empty;
            wfInstance.WF_TempId = id;
            WF_InstanceService.Add(wfInstance);

            // 2. Start the workflow
            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(new FinanceActivity(), null);
            wfInstance.WFInstanceId = wfApp.Id;     // save the workflow id 
            WF_InstanceService.Update(wfInstance);

            // 3. Add two new procedures to the workflow procedure
            // The first produce handler is self
            WF_Procedure startProcedure = new WF_Procedure();
            startProcedure.WF_InstanceId = wfInstance.Id;
            startProcedure.SubTime = DateTime.Now;
            startProcedure.ProcedureName = "Submit the approval table";
            startProcedure.IsEndProcedure = false;
            startProcedure.IsStartProcedure = true;
            startProcedure.HandleBy = currentUserId;
            startProcedure.ProcessComment = "submit approval";
            startProcedure.ProcessResult = "pass";
            startProcedure.ProcessStatus = (short)WF_InstanceEnum.Processing;
            WF_ProcedureService.Add(startProcedure);

            // Initial the next procedure handler
            WF_Procedure nextProcedure = new WF_Procedure();
            nextProcedure.WF_InstanceId = wfInstance.Id;
            nextProcedure.SubTime = DateTime.Now;
            nextProcedure.ProcessTime = DateTime.Now;
            nextProcedure.ProcessComment = string.Empty;

            nextProcedure.IsEndProcedure = false;
            nextProcedure.IsStartProcedure = false;
            nextProcedure.HandleBy = flowTo;

            nextProcedure.ProcessResult = "";
            nextProcedure.ProcessStatus = (short)WF_InstanceEnum.Unprocess;
            WF_ProcedureService.Add(nextProcedure);
            return RedirectToAction("showMyCheck");
        }
        #endregion


        #region Show
        public ActionResult ShowMyCheck()
        {
            var data = WF_InstanceService.GetEntities(i => i.SenderId == 1).ToList();
            return View(data);
        }

        public ActionResult ShowMyUnCheck()
        {
            // Get all unprocess Procedure whitch should be handle by the current user
            var procedure = WF_ProcedureService.GetEntities(u => u.ProcessStatus == (short)WF_InstanceEnum.Unprocess && u.HandleBy == 1).ToList();

            // Get the corresponding instance id 
            var instanceIds = (from p in procedure
                               select p.WF_InstanceId).Distinct();
            // Get the corresponding intance
            var data = WF_InstanceService.GetEntities(i => instanceIds.Contains(i.Id) && i.Status == (short)WF_InstanceEnum.Processing).ToList();

            return View(data);
        } 
        #endregion
    }
}